using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using TES3Lib.Records;
using TES3 = TES3Lib.TES3;
using static TES3Landgen.Utility;
using System.IO;
using TES3Lib.Subrecords.LAND;

namespace TES3Landgen
{
    public class TES3HeightMap
    {
        private const int CELL_SIZE = 65;
        private int width { get; set; }
        private int height { get; set; }
        private int maxX { get; set; }
        private int minX { get; set; }
        private int maxY { get; set; }
        private int minY { get; set; }
        private int offsetFromCenterX { get; set; }
        private int offsetFromCenterY { get; set; }
        private float HeightMax { get; set; }
        private float HeightMin { get; set; }
        private float[,] Heightmap { get; set; }
        private Rgb[,] Normals { get; set; }
        private Rgb[,] VertexColors { get; set; }
        private ushort[,] TexturePlacement { get; set; }
        private TES3 PluginRef { get; set; }

        private List<TES3> PluginsRef { get; set; }

        private Dictionary<int, string> LandTextures { get; set; }

        public TES3HeightMap(TES3 plugin)
        {
            PluginRef = plugin;
        }

        public TES3HeightMap(List<TES3> plugins)
        {
            PluginsRef = plugins;
        }

        public void ReadMapData(string output, ExportOptions mapOptions)
        {
            var land = GetLandRecords();
            LoadBaseInformation(land);
            GetLandTextures();

            if (mapOptions.HeightMap || mapOptions.NormalMap || mapOptions.VertexColorMap)
            {
                ImportMapsFromRecords(land, mapOptions);
            }

            if (mapOptions.VertexColorMap)
            {
                ImportTexturePlacement(land, output);
            }

            SaveMapData(output, mapOptions);
        }

        public void ValidateImportTesting(string output)
        {
            var test = RawImage.LoadGrayscale16($"{output}_hm.raw", width, height, (int)HeightMin);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (test[i, j] != Heightmap[i, j])
                    {
                        var dd = test[i, j];
                        var dd2 = Heightmap[i, j];
                    }
                }
            }
        }

        public void SaveMapData(string output, ExportOptions mapOptions)
        {
            if (mapOptions.HeightMap)
            {
                if (mapOptions.ExportHeightAsRaw)
                {
                    RawImage.Save($"{output}_hm", Heightmap, HeightMin, ColorDepth.Gray16);              
                    RawImage.SaveMetaData($"{output}_metadata", width, height, (int)HeightMin);

                    ImportMapsFromImage(output);
                }
                else
                {
                    SaveHeightmapAsBitmap($"{output}_hm.bmp", Heightmap, HeightMin, HeightMax);
                }           
            }

            if (mapOptions.NormalMap)
            {
                SaveMapAsBitmap($"{output}_nm", Normals);
            }

            if (mapOptions.VertexColorMap)
            {
                SaveMapAsBitmap($"{output}_vc", VertexColors);
            }

            if (mapOptions.TexturePlacementMap)
            {
                SaveTexturePlacementAsBitmap($"{output}_tex", TexturePlacement);
            }
        }

        private void GetLandTextures()
        {
            if (PluginRef != null)
            {
                LandTextures = new Dictionary<int, string>();
                LandTextures.Add(0, "#222222"); //default land texture
                foreach (LTEX texture in PluginRef.Records.Where(x => x.Name == "LTEX"))
                {
                    var hash = texture.NAME.EditorId.GetHashCode().ToString("X");
                    LandTextures.Add(texture.INTV.IndexNumber + 1, $"#{hash.Substring(0, hash.Length - 2)}");
                }

            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private List<LAND> GetLandRecords()
        {
            if (PluginRef != null)
            {
                return PluginRef.Records.Where(x => x.Name == "LAND").Cast<LAND>().ToList();
            }
            else
            {
                var merged = new HashSet<LAND>();
                for (int i = PluginsRef.Count - 1; i >= 0; i--)
                {
                    foreach (LAND land in PluginsRef[i].Records.Where(x => x.Name == "LAND"))
                    {
                        merged.Add(land);
                    }
                }
                return merged.ToList();
            }
        }

        private void LoadBaseInformation(List<LAND> records)
        {
            maxX = int.MinValue;
            maxY = int.MinValue;
            minX = int.MaxValue;
            minY = int.MaxValue;

            foreach (LAND land in records)
            {
               
                maxX = Math.Max(maxX, land.INTV.CellX);
                maxY = Math.Max(maxY, land.INTV.CellY);
                minX = Math.Min(minX, land.INTV.CellX);
                minY = Math.Min(minY, land.INTV.CellY);
            }
            offsetFromCenterX = Math.Max(Math.Abs(minX),0);
            offsetFromCenterY = Math.Max(Math.Abs(minY), 0);

            minX = minX > 0 ? 0 : minX;
            minY = minY > 0 ? 0 : minY;

            width = CalculateSideLength(maxX, minX) * CELL_SIZE;
            height = CalculateSideLength(maxY, minY) * CELL_SIZE;
        }

        private void ImportMapsFromRecords(List<LAND> records, ExportOptions exportOptions)
        {
            Heightmap = exportOptions.HeightMap ? new float[height, width] : null;
            Normals = exportOptions.NormalMap ? new Rgb[height, width] : null;
            VertexColors = exportOptions.VertexColorMap ? new Rgb[height, width] : null;

            HeightMax = float.MinValue;
            HeightMin = float.MaxValue;

            object @lock = new object();

            Parallel.ForEach(records, (land) =>
            {

                float heightOffsetRow = exportOptions.HeightMap && land.VHGT != null ? land.VHGT.HeightOffset : 0;
                for (int y = 0; y < CELL_SIZE; y++)
                {
                    float heightOffsetCol = 0;
                    for (int x = 0; x < CELL_SIZE; x++)
                    {

                        int cordX = CELL_SIZE * (Math.Abs(minX) + land.INTV.CellX) + x;
                        int cordY = CELL_SIZE * (Math.Abs(minY) + land.INTV.CellY) + y;

                        if (exportOptions.HeightMap && land.VHGT != null)
                        {
                            float heightPixel = 0;
                            if (x == 0)
                            {
                                heightPixel = land.VHGT.HeightDelta[y, x] + heightOffsetRow;
                                heightOffsetRow = heightPixel;
                                heightOffsetCol = heightPixel;
                            }
                            else
                            {
                                heightPixel = land.VHGT.HeightDelta[y, x] + heightOffsetCol;
                                heightOffsetCol = heightPixel;
                            }
                            Heightmap[cordY, cordX] = heightPixel;

                            lock (@lock)
                            {

                                HeightMax = Math.Max(HeightMax, heightPixel);
                                HeightMin = Math.Min(HeightMin, heightPixel);
                            }
                        }

                        if (exportOptions.NormalMap && land.VNML != null)
                        {
                            Normals[cordY, cordX].r = land.VNML.normals[y, x].x;
                            Normals[cordY, cordX].g = land.VNML.normals[y, x].y;
                            Normals[cordY, cordX].b = land.VNML.normals[y, x].z;
                        }

                        if (exportOptions.VertexColorMap && land.VCLR != null)
                        {
                            VertexColors[cordY, cordX].r = land.VCLR.VertexColors[y, x].r;
                            VertexColors[cordY, cordX].g = land.VCLR.VertexColors[y, x].g;
                            VertexColors[cordY, cordX].b = land.VCLR.VertexColors[y, x].b;
                        }
                    }
                }
                //if(land.VHGT != null) VHGTTester(land);              
            });
        }

        internal void VHGTTester(LAND land)
        {
            //testing
            int cordX = CELL_SIZE * (Math.Abs(minX) + land.INTV.CellX);
            int cordY = CELL_SIZE * (Math.Abs(minY) + land.INTV.CellY);

            var heightSubrecord = CreateHeightMapSubrecord(cordX, cordY, Heightmap);
            {//compare
                var check = land.VHGT.HeightOffset == heightSubrecord.HeightOffset &&
                land.VHGT.Unknown1 == heightSubrecord.Unknown1 && land.VHGT.Unknown2 == heightSubrecord.Unknown2;

                for (int i = 0; i < 65; i++)
                {
                    for (int j = 0; j < 65; j++)
                    {
                        if (check && land.VHGT.HeightDelta[i, j] != heightSubrecord.HeightDelta[i, j])
                        {
                            throw new Exception("failed");
                        }
                    }
                }
            }
        }

        private VHGT CreateHeightMapSubrecord(int offsetX,int offsetY, float[,] importedHeightMap)
        {
            var heightDeltas = new sbyte[CELL_SIZE, CELL_SIZE];
            for (int y = heightDeltas.GetLength(0)-1; y >=0; y--)
            {
                for (int x = heightDeltas.GetLength(1) - 1; x >= 0; x--)
                {
                    var y2 = offsetY + y;
                    var x2 = offsetX + x;

                    if (x==0)
                    {
                        if(y==0)
                        {
                            heightDeltas[y, x] = 0;
                            return new VHGT
                            {
                                HeightDelta = heightDeltas,
                                HeightOffset = importedHeightMap[y2, x2],
                            };
                        }
                        heightDeltas[y, x] = (sbyte)(importedHeightMap[y2, x2] - importedHeightMap[y2 - 1, x2]);
                        continue;
                    }

                    heightDeltas[y, x] = (sbyte)(importedHeightMap[y2, x2] - importedHeightMap[y2, x2 - 1]);
                }
            }
            return null;
        }

        private void ImportMapsFromImage(string output)
        {
            //var heightFromImage = RawImage.LoadGrayscale16($"{output}_hm.raw", width, height, (int)HeightMin);


            //for (int y = 0; y <= width; y+=65)
            //{
            //    for (int x = 0; x <= height; x+=65)
            //    {
   
                  
            //            ProcessSector(y1, x1, heightFromImage);
                
            //    }
            //}

            //Parallel.For(0, height, (j,statej) => {
            //    Parallel.For(0, width, (i,statei) => {
            //        int y = j * 65;
            //        int x = i * 65;

            //        if(y >= height || x >= width)
            //        {
            //            statej.Break();
            //            statei.Break();
            //        } else
            //        {
            //            ProcessSector(y, x, heightFromImage);
            //        }

                   
            //    });
            //});
        }

        private void SaveHeightmapAsBitmap(string outputPath, float[,] heightmap, float min, float max)
        {
            var bitmap = new Bitmap(this.width, this.height, PixelFormat.Format16bppGrayScale);
            for (int y = 0; y < heightmap.GetLength(0); y++)
            {
                for (int x = 0; x < heightmap.GetLength(1); x++)
                {
                    var normalisedHeight = (int)Normalise(heightmap[y, x], min, max, 0, 255);
                    var pixel = Color.FromArgb(normalisedHeight, normalisedHeight, normalisedHeight);
                    bitmap.SetPixel(x, y, pixel);
                }
            }

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save($"{outputPath}.bmp", ImageFormat.Bmp);
            bitmap.Dispose();
        }

        private void SaveMapAsBitmap(string outputPath, Rgb[,] map)
        {
            var bitmap = new Bitmap(this.width, this.height);
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    var pixel = Color.FromArgb(map[y, x].r, map[y, x].g, map[y, x].b);
                    bitmap.SetPixel(x, y, pixel);
                }
            }

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save($"{outputPath}.bmp", ImageFormat.Bmp);
            bitmap.Dispose();
        }

        private void ImportTexturePlacement(List<LAND> records, string outputPath)
        {
            TexturePlacement = new ushort[16 * height / CELL_SIZE, 16 * width / CELL_SIZE];

            const bool forceNonParallel = false;
            var options = new ParallelOptions { MaxDegreeOfParallelism = forceNonParallel ? 1 : -1 };
            Parallel.ForEach(records, options, (land) =>
            {
                if (land.VTEX == null) return;

                ushort[,] texTransformed = TransformRowsToBlock(land.VTEX.TexIndices);

                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        int cordX = 16 * (Math.Abs(minX) + land.INTV.CellX) + x;
                        int cordY = 16 * (Math.Abs(minY) + land.INTV.CellY) + y;

                        TexturePlacement[cordY, cordX] = texTransformed[y, x];
                    }
                }
            });

            SaveTexturePlacementAsBitmap(outputPath, TexturePlacement);
        }

        private void SaveTexturePlacementAsBitmap(string outputPath, ushort[,] texturePlacement)
        {
            var bitmap = new Bitmap(16 * (width / CELL_SIZE), 16 * (height / CELL_SIZE));
            for (int y = 0; y < texturePlacement.GetLength(0); y++)
            {
                for (int x = 0; x < texturePlacement.GetLength(1); x++)
                {
                    var inx = texturePlacement[y, x];

                    var colorHex = LandTextures[inx];
                    bitmap.SetPixel(x, y, ColorTranslator.FromHtml(colorHex));
                }
            }

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save(outputPath, ImageFormat.Bmp);
            bitmap.Dispose();
        }
    }

    /// <summary>
    /// Configuration of export options
    /// </summary>
    public class ExportOptions
    {
        /// <summary>
        /// Export heightmap data as grayscale image (1 cell is 65x65 pixels)
        /// </summary>
        public bool HeightMap { get; set; }

        /// <summary>
        /// Export lands normal map as RGB image (1 cell is 65x65 pixels)
        /// </summary>
        public bool NormalMap { get; set; }

        /// <summary>
        /// Export lands vertex coloring as RGB image  (1 cell is 65x65 pixels)
        /// </summary>
        public bool VertexColorMap { get; set; }

        /// <summary>
        /// Export texture placement map as RGB image (1 cell is 16x16 pixels)
        /// Where 1 pixel is equals to 4 pixels of heightmap
        /// </summary>
        public bool TexturePlacementMap { get; set; }

        /// <summary>
        /// Export as raw 16bit grayscale image
        /// </summary>
        public bool ExportHeightAsRaw { get; set; }
    }
}
