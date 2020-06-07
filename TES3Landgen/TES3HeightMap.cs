using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using TES3Lib.Records;
using TES3 = TES3Lib.TES3;

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
        private float[,] Normals { get; set; }
        private float[,] VertexColors { get; set; }
        private ushort[,] TexturePlacement { get; set; }
        private TES3 PluginRef { get; set; }

        private List<TES3> PluginsRef { get; set; }

        private Dictionary<int, string> LandTextures {get;set;}

        public TES3HeightMap(TES3 plugin)
        {
            PluginRef = plugin;
        }

        public TES3HeightMap(List<TES3> plugins)
        {
            PluginsRef = plugins;
        }

        public void ExportHeightmap(string output)
        {
            var land = GetLandRecords();
            LoadBaseInformation(land);
            GetLandTextures();
            //ExportMaps(land,output,heightmap:true);
            ExportLandTexturePlacement(land, output);
        }

        private void GetLandTextures()
        {
            if (PluginRef != null)
            {
                LandTextures = new Dictionary<int, string>();
                foreach (LTEX texture in PluginRef.Records.Where(x => x.Name == "LTEX"))
                {

                    var hash = texture.NAME.EditorId.GetHashCode().ToString("X");

                    LandTextures.Add((ushort)texture.INTV.IndexNumber, hash.Substring(0, hash.Length - 2));
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private List<LAND> GetLandRecords()
        {
            if(PluginRef!=null)
            {
                return PluginRef.Records.Where(x => x.Name == "LAND").Cast<LAND>().ToList();
            }
            else
            {
                var merged = new HashSet<LAND>();
                for (int i = PluginsRef.Count-1; i >= 0; i--)
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

                maxX = maxX > land.INTV.CellX ? maxX : land.INTV.CellX;
                maxY = maxY > land.INTV.CellY ? maxY : land.INTV.CellY;
                minX = minX < land.INTV.CellX ? minX : land.INTV.CellX;
                minY = minY < land.INTV.CellY ? minY : land.INTV.CellY;
            }
            offsetFromCenterX = minX > 0 ? minX : 0;
            offsetFromCenterY = minY > 0 ? minY : 0;

            minX = minX > 0 ? 0 : minX;
            minY = minY > 0 ? 0 : minY;

            width = CalculateSideLength(maxX, minX)*CELL_SIZE;
            height = CalculateSideLength(maxY, minY)*CELL_SIZE;
        }

        private void ExportMaps(List<LAND> records,string outputPath,bool heightmap=false,bool normalmap=false,bool vcolmap=false)
        {
            Heightmap = new float[width, height];
            //Normals = new float[width, height];
            //VertexColors = new float[width, height];

            HeightMax = float.MinValue;
            HeightMin = float.MaxValue;

            object guardian = new object();

            Parallel.ForEach(records, (land) =>
            {
                if (land.VHGT == null) return;

                float heightOffsetRow = heightmap ? land.VHGT.HeightOffset : 0;
                for (int y = 0; y < CELL_SIZE; y++)
                {
                    float heightOffsetCol = 0;
                    for (int x = 0; x < CELL_SIZE; x++)
                    {

                        int cordX = CELL_SIZE * (Math.Abs(minX) + land.INTV.CellX) + x;
                        int cordY = CELL_SIZE * (Math.Abs(minY) + land.INTV.CellY) + y;

                        if(heightmap)
                        {
                            ProcessHeightData(land, guardian, ref heightOffsetRow, y, ref heightOffsetCol, x, cordX, cordY);
                        }
                    }
                }
            });

            if(heightmap)
            {
                SaveHeightmapAsBitmap(outputPath, Heightmap, HeightMin, HeightMax);
            }    
        }

        private void ProcessHeightData(LAND land, object lockObj, ref float heightOffsetRow, int y, ref float heightOffsetCol, int x, int cordX, int cordY)
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
            Heightmap[cordX, cordY] = heightPixel;

            lock (lockObj)
            {
                HeightMax = HeightMax < heightPixel ? heightPixel : HeightMax;
                HeightMin = HeightMin > heightPixel ? heightPixel : HeightMin;
            }
        }

        private void SaveHeightmapAsBitmap(string outputPath,float[,] heightmap, float min, float max)
        {
            var bitmap = new Bitmap(this.width,this.height);
            for (int y = 0; y < heightmap.GetLength(0); y++)
            {
                for (int x = 0; x < heightmap.GetLength(1); x++)
                {
                    var normalisedHeight = (int)Normalise(heightmap[y, x], min, max, 0, 255);
                    var pixel = Color.FromArgb(normalisedHeight, normalisedHeight, normalisedHeight);
                    bitmap.SetPixel(y, x, pixel);
                }
            }

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save(outputPath, ImageFormat.Bmp);
            bitmap.Dispose();
        }

        private void ExportLandTexturePlacement(List<LAND> records, string outputPath)
        {
            TexturePlacement = new ushort[16*width/CELL_SIZE, 16*height/CELL_SIZE];

            Parallel.ForEach(records, (land) =>
            {
                if (land.VTEX == null) return;

                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {

                        int cordX = 16 * (Math.Abs(minX) + land.INTV.CellX) + x;
                        int cordY = 16 * (Math.Abs(minY) + land.INTV.CellY) + y;

                        TexturePlacement[cordX, cordY] = land.VTEX.TexIndices[y,x];
                    }
                }
            });

            var bitmap = new Bitmap(16 * (width / CELL_SIZE), 16 * (height / CELL_SIZE));
            for (int y = 0; y < TexturePlacement.GetLength(0); y++)
            {
                for (int x = 0; x < TexturePlacement.GetLength(1); x++)
                {
                    var inx = TexturePlacement[y, x] >= 107 ? 0 : TexturePlacement[y, x];
                    var colorHex = LandTextures[inx];
                    bitmap.SetPixel(y, x, ColorTranslator.FromHtml($"#{colorHex}"));
                }
            }

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save(outputPath, ImageFormat.Bmp);
            bitmap.Dispose();
        }

        #region utils
        private int CalculateSideLength(int max, int min)
        {
            var sideLength = (1 + max - min) * (1 + max - min);
            return (int)Math.Sqrt(sideLength);
        }
        private static float Normalise(float value, float rangeMin, float rangeMax, float scaledRangeMin, float scaledRangeMax) =>
            scaledRangeMin + (value - rangeMin) / (rangeMax - rangeMin) * (scaledRangeMax - scaledRangeMin);
        #endregion
    }
}
