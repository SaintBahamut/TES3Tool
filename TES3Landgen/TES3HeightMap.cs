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
using Newtonsoft.Json;
using TES3Lib.Base;

namespace TES3Landgen
{
    public class TES3HeightMap
    {
        #region parameters
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
        #endregion

        #region cosntructors
        public TES3HeightMap()
        {

        }

        public TES3HeightMap(TES3 plugin)
        {
            PluginRef = plugin;
        }

        public TES3HeightMap(List<TES3> plugins)
        {
            PluginsRef = plugins;
        }
        #endregion

        #region base read and save
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
            offsetFromCenterX = Math.Max(Math.Abs(minX), 0);
            offsetFromCenterY = Math.Max(Math.Abs(minY), 0);

            minX = minX > 0 ? 0 : minX;
            minY = minY > 0 ? 0 : minY;

            width = CalculateSideLength(maxX, minX) * CELL_SIZE;
            height = CalculateSideLength(maxY, minY) * CELL_SIZE;
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

            if (mapOptions.TexturePlacementMap)
            {
                ImportTexturePlacement(land);
            }

            SaveMapData(output, mapOptions);
        }

        public void SaveMapData(string output, ExportOptions mapOptions)
        {
            if (mapOptions.HeightMap)
            {
                if (mapOptions.ExportHeightAsRaw)
                {
                    RawImage.SaveMap($"{output}_hm", Heightmap, HeightMin, ColorDepth.Gray16);
                    RawImage.SaveRawMetadataToJson($"{output}_hm_metadata", width, height, (int)HeightMin);

                    //ValidateImportTesting(output);
                }
                else
                {
                    BitmapImage.SaveHeightmap($"{output}_hm", Heightmap, HeightMin, HeightMax);
                }
            }

            if (mapOptions.NormalMap)
            {
                BitmapImage.SaveRGB($"{output}_nm", Normals);
            }

            if (mapOptions.VertexColorMap)
            {
                BitmapImage.SaveRGB($"{output}_vc", VertexColors);
            }

            if (mapOptions.TexturePlacementMap)
            {
                Utility.SaveTexturePlacementDictionaryAsJson($"{output}_tx_metadata", LandTextures);
                BitmapImage.SaveRGBFromIndexPalette($"{output}_tx", TexturePlacement, LandTextures);
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
        #endregion

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
                                
                                if(HeightMin < 0)
                                {
                                    { }
                                }

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
                //if (land.VHGT != null) VHGTTester(land);
            });
        }

        private void ImportTexturePlacement(List<LAND> records)
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
        }

        public void ImportMapFromImage(string path, int moveX = 0, int moveY = 0)
        {
            var heightMeta = RawImage.LoadRawMetadataFromJson(path);
            var heightData = RawImage.LoadGrayscale16(path, heightMeta);

            int cellNumberX = heightData.GetLength(1) / 65;
            int cellNumberY = heightData.GetLength(0) / 65;

            var landList = new List<Record>();

            for (int y = 0; y < cellNumberY; y++)
            {
                for (int x = 0; x < cellNumberX; x++)
                {
                    var landRecord = new LAND();
                    landRecord.INTV = new INTV
                    {
                        CellX = x + moveX,
                        CellY = y + moveY,
                    };
                    landRecord.DATA = new DATA();
                    landRecord.VHGT = CreateHeightMapSubrecord(x * CELL_SIZE, y * CELL_SIZE, heightData);
                    landRecord.VNML = CreateVertexNormalSubrecord(x * CELL_SIZE, y * CELL_SIZE, heightData);


                    var cellRecord = new CELL
                    {
                        NAME = new TES3Lib.Subrecords.Shared.NAME
                        {
                            EditorId = ""
                        },
                        DATA = new TES3Lib.Subrecords.CELL.DATA
                        {
                            Flags = new HashSet<TES3Lib.Enums.Flags.CellFlag>(),
                            GridX = landRecord.INTV.CellX,
                            GridY = landRecord.INTV.CellY,
                        },
                    };

                    landList.Add(cellRecord);
                    landList.Add(landRecord);
                }
            }

            var example = new TES3();
            example.Records.Add(createTES3HEader());
            example.Records.AddRange(landList);
            example.TES3Save($"{path}.esp");
        }

        #region subrecord_creators
        private VHGT CreateHeightMapSubrecord(int offsetX, int offsetY, float[,] importedHeightMap)
        {
            var heightDeltas = new sbyte[CELL_SIZE, CELL_SIZE];

            for (int y = heightDeltas.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = heightDeltas.GetLength(1) - 1; x >= 0; x--)
                {
                    var y2 = offsetY + y;
                    var x2 = offsetX + x;

                    if (x == 0)
                    {
                        if (y == 0)
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

                    var grad = (importedHeightMap[y2, x2] - importedHeightMap[y2, x2 - 1]);

                    heightDeltas[y, x] = (sbyte)grad;
                }
            }
            return null;
        }

        private VNML CreateVertexNormalSubrecord(int offsetX, int offsetY, float[,] importedHeightMap)
        {
            var normals = new normal[CELL_SIZE, CELL_SIZE];
            for (int y = 0 - 1; y >= 0; y--)
            {
                for (int x = normals.GetLength(1) - 1; x >= 0; x--)
                {
                    var y2 = offsetY + y;
                    var x2 = offsetX + x;
                    var v1 = new float[] { 16, 0, importedHeightMap[y2, x2 + 1] - importedHeightMap[y2, x2] };
                    var v2 = new float[] { 0, 16, importedHeightMap[y2 + 1, x2] - importedHeightMap[y2, x2] };

                    double vx = v1[1] * v2[2] - v1[2] * v2[1];
                    double vy = v1[2] * v2[0] - v1[0] * v2[2];
                    double vz = v1[0] * v2[1] - v1[1] * v2[0];
                    double hyp = Math.Sqrt(vx * vx + vy * vy + vz * vz) / 127.0f;

                    normals[x, y].x = (byte)(vx / hyp);
                    normals[x, y].y = (byte)(vy / hyp);
                    normals[x, y].z = (byte)(vz / hyp);
                }
            }
            return new VNML { normals = normals };
        }

        private VTEX CreateTexturePlacementSubrecord(int offsetX, int offsetY, ushort[,] importedTextureData)
        {
            var texturePlacement = new ushort[16, 16];
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    var y2 = offsetY + y;
                    var x2 = offsetX + x;

                    texturePlacement[y, x] = importedTextureData[y2, x2];
                }
            };

            return new VTEX { TexIndices = TransformBlocksToRows(texturePlacement) };
        }
        #endregion

        #region junkyard
        public void ValidateImportTesting(string output)
        {
            var dd33 = new RawImageParams
            {
                height = height,
                width = width,
                heightScaleMultiplier = 8,
                zeroOffset = -269
            };

            var test = RawImage.LoadGrayscale16($"{output}_hm", dd33);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (test[i, j] != Heightmap[i, j])
                    {
                        var dd = test[i, j];
                        var dd2 = Heightmap[i, j];
                        { }
                    }
                }
            }

            RawImage.SaveMap($"{output}_rawr", test, HeightMin, ColorDepth.Gray16);
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
        #endregion
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
