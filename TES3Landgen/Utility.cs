using System;
using System.IO;

namespace TES3Landgen
{
    internal static class Utility
    {
        internal static ushort[,] TransformRowsToBlock(ushort[,] texPositionArray)
        {
            var texTransformed = new ushort[16, 16];
            int posX = 0;
            int posY = 0;
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    texTransformed[posY, posX] = texPositionArray[y, x];
                    posX++;
                    if ((posX % 4) == 0)
                    {
                        posX -= 4;
                        posY++;
                    }
                }
                posX += 4;
                if (posX >= 16)
                {

                    posX = 0;
                }
                else
                {
                    posY -= 4;
                }
            }

            return texTransformed;
        }

        internal static int CalculateSideLength(int max, int min)
        {
            var sideLength = (1 + max - min) * (1 + max - min);
            return (int)Math.Sqrt(sideLength);
        }

        internal static float Normalise(float value, float rangeMin, float rangeMax, float scaledRangeMin, float scaledRangeMax) =>
            scaledRangeMin + (value - rangeMin) / (rangeMax - rangeMin) * (scaledRangeMax - scaledRangeMin);
    }

    public enum ColorDepth : byte
    {
        Gray16 = 16
    }


    internal static class RawImage
    {
        internal static void Save(string path, float[,] pixels, float zeroOffset, ColorDepth colorDepth)
        {
            const int mwScaleUnit = 8;

            using (var writer = new BinaryWriter(File.Open($"{path}.raw", FileMode.Create)))
            {
                for (int i = pixels.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = 0; j < pixels.GetLength(1); j++)
                    {
                        if (colorDepth == ColorDepth.Gray16)
                        {
                            ushort normalised = (ushort)((pixels[i, j] - zeroOffset) * mwScaleUnit);
                            writer.Write(normalised);
                        }
                    }
                }
            }
        }

        internal static float[,] LoadGrayscale16(string path, int width, int height, int offset)
        {
            var reader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read));
            var image = new float[height, width];
            for (int i = image.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    var pix = reader.ReadUInt16();
                    image[i, j] = pix - offset;
                }
            }
            reader.Dispose();
            return image;
        }

        internal static void SaveMetaData(string path, int width, int height, int zeroOffset)
        {
            //using (var writer = new StreamWriter(File.Open($"{path}_meta.json", FileMode.Create)))
            //{
            // TODO save metadata to json
            //}
            Console.WriteLine($"height: {height}");
            Console.WriteLine($"width: {width}");
            Console.WriteLine($"offset from sea level: {zeroOffset}");
        }
    }

    internal struct Rgb
    {
        public byte r { get; set; }
        public byte g { get; set; }
        public byte b { get; set; }
    }
}
