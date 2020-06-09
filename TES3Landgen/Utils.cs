using System;

namespace TES3Landgen
{
    internal static class Utils
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

    internal struct Rgb
    {
        public byte r { get; set; }
        public byte g { get; set; }
        public byte b { get; set; }
    }
}
