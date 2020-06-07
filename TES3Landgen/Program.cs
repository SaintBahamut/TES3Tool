using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using TES3Lib;

namespace TES3Landgen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MW Land Load Test");
            MWLoadTest();
        }


        public static void MWLoadTest()
        {
            string fileESM = "C:\\Software\\Steam\\steamapps\\common\\Morrowind\\Data Files\\Morrowind.esm";
            //string BM = "C:\\Software\\Steam\\steamapps\\common\\Morrowind\\Data Files\\Bloodmoon.esm";
            var timer = Stopwatch.StartNew();
            TES3 tes3 = TES3.TES3Load(fileESM, new List<string> { "LAND","LTEX" });
            //TES3 bm = TES3.TES3Load(BM, new List<string> { "LAND","LTEX" });

            var heightmap = new TES3HeightMap(tes3);

            heightmap.ExportHeightmap("output.bmp");
            timer.Stop();

            Console.WriteLine($"Done in {timer.ElapsedMilliseconds} ms");
        }

        public static void LandNormals2BMP(TES3 tes3)
        {
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            foreach (TES3Lib.Records.LAND land in tes3.Records.Where(x => x.Name == "LAND"))
            {
                if (land.VNML == null) continue;

                maxX = maxX > land.INTV.CellX ? maxX : land.INTV.CellX;
                maxY = maxY > land.INTV.CellY ? maxY : land.INTV.CellY;
                minX = minX < land.INTV.CellX ? minX : land.INTV.CellX;
                minY = minY < land.INTV.CellY ? minY : land.INTV.CellY;
            }
            int offsetFromCenterX = minX > 0 ? minX : 0;
            int offsetFromCenterY = minY > 0 ? minY : 0;

            minX = minX > 0 ? 0 : minX;
            minY = minY > 0 ? 0 : minY;

            int normalCellSize = 65;


            int width = (1 + maxX - minX) * (1 + maxX - minX);
            width = (int)Math.Sqrt(width) * normalCellSize;

            int height = (1 + maxY - minY) * (1 + maxY - minY);
            height = (int)Math.Sqrt(height) * normalCellSize;

            var bitmap = new Bitmap(width, height);

            foreach (TES3Lib.Records.LAND land in tes3.Records.Where(x => x.Name == "LAND"))
            {
                if (land.VNML == null) continue;

                for (int i = 0; i < land.VNML.normals.GetLength(0); i++)
                {
                    for (int j = 0; j < land.VNML.normals.GetLength(1); j++)
                    {
                        int cordX = 65 * (Math.Abs(minX) + land.INTV.CellX) + i;
                        int cordY = 65 * (Math.Abs(minY) + land.INTV.CellY) + j;
                        var pixel = Color.FromArgb(land.VNML.normals[i, j].x, land.VNML.normals[i, j].y, land.VNML.normals[i, j].z);
                        bitmap.SetPixel(cordX, cordY, pixel);
                    }
                }
            }
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save("output.bmp", ImageFormat.Bmp);
            Console.WriteLine($"Dimensions {width} x {height}");
        }

        public static void LoadVertexColors2BMP(TES3 tes3)
        {
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            foreach (TES3Lib.Records.LAND land in tes3.Records.Where(x => x.Name == "LAND"))
            {
                if (land.VCLR == null) continue;

                maxX = maxX > land.INTV.CellX ? maxX : land.INTV.CellX;
                maxY = maxY > land.INTV.CellY ? maxY : land.INTV.CellY;
                minX = minX < land.INTV.CellX ? minX : land.INTV.CellX;
                minY = minY < land.INTV.CellY ? minY : land.INTV.CellY;
            }
            int offsetFromCenterX = minX > 0 ? minX : 0;
            int offsetFromCenterY = minY > 0 ? minY : 0;

            minX = minX > 0 ? 0 : minX;
            minY = minY > 0 ? 0 : minY;

            int normalCellSize = 65;


            int width = (1 + maxX - minX) * (1 + maxX - minX);
            width = (int)Math.Sqrt(width) * normalCellSize;

            int height = (1 + maxY - minY) * (1 + maxY - minY);
            height = (int)Math.Sqrt(height) * normalCellSize;

            var bitmap = new Bitmap(width, height);

            foreach (TES3Lib.Records.LAND land in tes3.Records.Where(x => x.Name == "LAND"))
            {
                if (land.VCLR == null) continue;

                for (int i = 0; i < land.VCLR.VertexColors.GetLength(0); i++)
                {
                    for (int j = 0; j < land.VCLR.VertexColors.GetLength(1); j++)
                    {
                        int cordX = 65 * (Math.Abs(minX) + land.INTV.CellX) + i;
                        int cordY = 65 * (Math.Abs(minY) + land.INTV.CellY) + j;
                        var pixel = Color.FromArgb(land.VCLR.VertexColors[i, j].r, land.VCLR.VertexColors[i, j].g, land.VCLR.VertexColors[i, j].b);
                        bitmap.SetPixel(cordX, cordY, pixel);
                    }
                }
            }
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save("output.bmp", ImageFormat.Bmp);
            Console.WriteLine($"Dimensions {width} x {height}");
        }

        public static void LoadHeightMap2BMP(TES3 tes3)
        {
            var maxX = int.MinValue;
            var maxY = int.MinValue;
            var minX = int.MaxValue;
            var minY = int.MaxValue;

            foreach (TES3Lib.Records.LAND land in tes3.Records.Where(x => x.Name == "LAND"))
            {
                if (land.VHGT == null) continue;

                maxX = maxX > land.INTV.CellX ? maxX : land.INTV.CellX;
                maxY = maxY > land.INTV.CellY ? maxY : land.INTV.CellY;
                minX = minX < land.INTV.CellX ? minX : land.INTV.CellX;
                minY = minY < land.INTV.CellY ? minY : land.INTV.CellY;
            }
            int offsetFromCenterX = minX > 0 ? minX : 0;
            int offsetFromCenterY = minY > 0 ? minY : 0;

            minX = minX > 0 ? 0 : minX;
            minY = minY > 0 ? 0 : minY;

            int normalCellSize = 65;


            int width = (1 + maxX - minX) * (1 + maxX - minX);
            width = (int)Math.Sqrt(width) * normalCellSize;

            int height = (1 + maxY - minY) * (1 + maxY - minY);
            height = (int)Math.Sqrt(height) * normalCellSize;

            var bitmap = new Bitmap(width, height);

            var heightValues = new float[width, height];

            var max = 0f;
            var min = 0f;

            foreach (TES3Lib.Records.LAND land in tes3.Records.Where(x => x.Name == "LAND"))
            {
                if (land.VHGT == null) continue;

                float heightOffsetRow = land.VHGT.HeightOffset;
                for (int y = 0; y < land.VHGT.HeightDelta.GetLength(0); y++)
                {
                    float heightOffsetCol=0;
                    for (int x = 0; x < land.VHGT.HeightDelta.GetLength(1); x++)
                    {

                        int cordX = 65 * (Math.Abs(minX) + land.INTV.CellX) + x;
                        int cordY = 65 * (Math.Abs(minY) + land.INTV.CellY) + y;

                        float heightPixel=0;
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

                        heightValues[cordX, cordY] = heightPixel;
                        max = max < heightPixel ? heightPixel : max;
                        min = min > heightPixel ? heightPixel : min;                       
                    }
                }
            }

            for (int i = 0; i < heightValues.GetLength(0); i++)
            {
                for (int j = 0; j < heightValues.GetLength(1); j++)
                {
                    var normalisedHeight = (int)Scale(heightValues[i, j], min, max, 0, 255);
                    var pixel = Color.FromArgb(normalisedHeight, normalisedHeight, normalisedHeight);
                    bitmap.SetPixel(i, j, pixel);
                }
            }
        


            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save("output.bmp", ImageFormat.Bmp);
            Console.WriteLine($"Dimensions {width} x {height}");
        }

        public static void LoadHeightMap2BMP2(TES3 tes3)
        {
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            foreach (TES3Lib.Records.LAND land in tes3.Records.Where(x => x.Name == "LAND"))
            {
                if (land.VHGT == null) continue;

                maxX = maxX > land.INTV.CellX ? maxX : land.INTV.CellX;
                maxY = maxY > land.INTV.CellY ? maxY : land.INTV.CellY;
                minX = minX < land.INTV.CellX ? minX : land.INTV.CellX;
                minY = minY < land.INTV.CellY ? minY : land.INTV.CellY;
            }
            int offsetFromCenterX = minX > 0 ? minX : 0;
            int offsetFromCenterY = minY > 0 ? minY : 0;

            minX = minX > 0 ? 0 : minX;
            minY = minY > 0 ? 0 : minY;

            int normalCellSize = 65;


            int width = (1 + maxX - minX) * (1 + maxX - minX);
            width = (int)Math.Sqrt(width) * normalCellSize;

            int height = (1 + maxY - minY) * (1 + maxY - minY);
            height = (int)Math.Sqrt(height) * normalCellSize;

            var bitmap = new Bitmap(width, height);

            var heightValues = new float[width, height];

            var max = 0f;
            var min = 0f;


            foreach (TES3Lib.Records.LAND land in tes3.Records.Where(x => x.Name == "LAND"))
            {
                if (land.VHGT == null) continue;


                float heightOffsetRow = land.VHGT.HeightOffset;
                for (int y= 0; y < 65; y++)
                {
                    heightOffsetRow += land.VHGT.HeightVector[y*65];

                    int cordX = 65 * (Math.Abs(minX) + land.INTV.CellX);
                    int cordY = 65 * (Math.Abs(minY) + land.INTV.CellY) + y;

                    max = max < heightOffsetRow*8 ? heightOffsetRow*8 : max;
                    min = min > heightOffsetRow*8 ? heightOffsetRow*8 : min;

                    heightValues[cordX, cordY] = heightOffsetRow * 8;

                    float heightOffsetCol = heightOffsetRow;
                    for (int x = 1; x < 65; x++)
                    {
                        heightOffsetCol += land.VHGT.HeightVector[y * 65 + x];

                        cordX = 65 * (Math.Abs(minX) + land.INTV.CellX) + x;                      
                        heightValues[cordX, cordY] = heightOffsetCol * 8;

                        max = max < heightOffsetCol * 8 ? heightOffsetCol * 8 : max;
                        min = min > heightOffsetCol * 8 ? heightOffsetCol * 8 : min;            
                    }
                }
            }

            for (int i = 0; i < heightValues.GetLength(0); i++)
            {
                for (int j = 0; j < heightValues.GetLength(1); j++)
                {
                    var normalisedHeight = (int)Scale(heightValues[i, j], min, max, 0, 255);
                    var pixel = Color.FromArgb(normalisedHeight, normalisedHeight, normalisedHeight);
                    bitmap.SetPixel(i, j, pixel);
                }
            }



            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save("output.bmp", ImageFormat.Bmp);
            Console.WriteLine($"Dimensions {width} x {height}");
        }

        public static float Scale(float elementToScale, float rangeMin, float rangeMax, float scaledRangeMin, float scaledRangeMax)
        {
            var scaled = scaledRangeMin + (elementToScale - rangeMin) / (rangeMax - rangeMin) * (scaledRangeMax - scaledRangeMin);
            return scaled;
        }
    }
}
