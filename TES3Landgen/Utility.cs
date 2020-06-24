using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TES3Landgen
{
    internal static class Utility
    {
        public static ushort[,] TransformRowsToBlock(ushort[,] texPositionArray)
        {
            var texTransformed = new ushort[16, 16];
            int subX = 0;
            int subY = 0;
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    texTransformed[subY, subX] = texPositionArray[y, x];
                    subX++;
                    if ((subX % 4) == 0)
                    {
                        subX -= 4;
                        subY++;
                    }
                }
                subX += 4;
                if (subX >= 16)
                {
                    subX = 0;
                }
                else
                {
                    subY -= 4;
                }
            }

            return texTransformed;
        }

        public static ushort[,] TransformBlocksToRows(ushort[,] texPositionArray)
        {
            var texTransformed = new ushort[16, 16];

            int subX = 0; int subY = 0;

            for (int y = 0; y < 16; y++)
            {

                subY = (y % 4 == 0) ? y : subY - 4;
                subX = (y % 4) * 4;

                for (int x = 0; x < 16; x++)
                {
                    texTransformed[y, x] = texPositionArray[subY, subX];

                    subX++;

                    if (subX % 4 == 0)
                    {
                        subX -= 4;
                        subY++;
                    }
                }
            }

            return texTransformed;
        }

        public static int CalculateSideLength(int max, int min)
        {
            var sideLength = (1 + max - min) * (1 + max - min);
            return (int)Math.Sqrt(sideLength);
        }

        public static float Normalise(float value, float rangeMin, float rangeMax, float scaledRangeMin, float scaledRangeMax) =>
            scaledRangeMin + (value - rangeMin) / (rangeMax - rangeMin) * (scaledRangeMax - scaledRangeMin);

        public static void SaveTexturePlacementDictionaryAsJson(string output, Dictionary<int, string> landTextures)
        {
            var jsonText = JsonConvert.SerializeObject(landTextures, Formatting.Indented);
            File.WriteAllText($"{output}.json", jsonText);
        }

        internal static TES3Lib.Records.TES3 createTES3HEader()
        {
            var header = new TES3Lib.Records.TES3
            {
                HEDR = new TES3Lib.Subrecords.TES3.HEDR
                {
                    CompanyName = "TES3Landgen\0",
                    Description = "\0",
                    NumRecords = 666,
                    ESMFlag = 0,
                    Version = 1.3f,
                },
            };
            header.Masters = new List<(TES3Lib.Subrecords.TES3.MAST MAST, TES3Lib.Subrecords.TES3.DATA DATA)>();
            header.Masters.Add((new TES3Lib.Subrecords.TES3.MAST { Filename = "Morrowind.esm\0" }, new TES3Lib.Subrecords.TES3.DATA { MasterDataSize = 6666 }));

            return header;
        }

        public static Dictionary<int, string> LoadTexturePlacementDictionaryFromJson(string input, Dictionary<int, string> landTextures)
        {
            string json = File.ReadAllText($"{input}_tex_dictionary.json");
            return JsonConvert.DeserializeObject<Dictionary<int, string>>(json);     
        }
    }

    public enum ColorDepth : byte
    {
        Gray16 = 16
    }

    internal static class RawImage
    {
        const int mwScaleUnit = 8;

        const int CELL_SIZE = 65;

        /// <summary>
        /// Saves
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pixels"></param>
        /// <param name="zeroOffset"></param>
        /// <param name="colorDepth"></param>
        public static void SaveMap(string path, float[,] pixels, float zeroOffset, ColorDepth colorDepth, int heightScaleMultiplier=8)
        {

            using (var writer = new BinaryWriter(File.Open($"{path}.raw", FileMode.Create)))
            {
                for (int i = pixels.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = 0; j < pixels.GetLength(1); j++)
                    {
                        if (colorDepth == ColorDepth.Gray16)
                        {
                            if (j == 552 && i == 2217)
                                Console.WriteLine(pixels[i, j]);

                            ushort normalised = (ushort)((pixels[i, j] + Math.Abs(zeroOffset)) * heightScaleMultiplier);

                            if (j == 552 && i == 2217)
                                Console.WriteLine(normalised);

                            writer.Write(normalised);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves texture placemnt map
        /// </summary>
        /// <param name="output"></param>
        /// <param name="texturePlacement"></param>
        public static float[,] LoadGrayscale16(string path, RawImageParams rawParams )
        {
            var pathFix = path.EndsWith(".raw") ? path : $"{path}.raw";
            var reader = new BinaryReader(File.Open(pathFix, FileMode.Open, FileAccess.Read));
            var image = new float[rawParams.height, rawParams.width];
            for (int i = image.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    var pix = reader.ReadUInt16();

                    if (j == 552 && i == 2217)
                        Console.WriteLine(pix);


                    image[i, j] = (pix / rawParams.heightScaleMultiplier) + rawParams.zeroOffset;

                    if (j == 552 && i == 2217)
                        Console.WriteLine(image[i, j]);
                }
            }
            reader.Dispose();
            return image;
        }

        public static void SaveRawMetadataToJson(string path, int width, int height, int zeroOffset)
        {
            var rawParams =  new RawImageParams { width=width, height=height, zeroOffset=zeroOffset };
            var jsonText = JsonConvert.SerializeObject(rawParams);
            File.WriteAllText($"{path}.json", jsonText);
        }

        public static RawImageParams LoadRawMetadataFromJson(string path)
        {
            string json = File.ReadAllText($"{path}.json");
            return JsonConvert.DeserializeObject<RawImageParams>(json);
        }
    }

    internal static class BitmapImage
    {
        const int CELL_SIZE = 65;

        public static void SaveRGBFromIndexPalette(string output, ushort[,] texturePlacement, Dictionary<int, string> palette)
        {
            var bitmap = new Bitmap(texturePlacement.GetLength(1) , texturePlacement.GetLength(0));
            for (int y = 0; y < texturePlacement.GetLength(0); y++)
            {
                for (int x = 0; x < texturePlacement.GetLength(1); x++)
                {
                    var inx = texturePlacement[y, x];

                    var colorHex = palette[inx];
                    bitmap.SetPixel(x, y, ColorTranslator.FromHtml(colorHex));
                }
            }

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save($"{output}_tex.bmp", ImageFormat.Bmp);
            bitmap.Dispose();
        }

        public static void SaveRGB(string outputPath, Rgb[,] map)
        {
            var bitmap = new Bitmap(map.GetLength(1), map.GetLength(2));
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

        public static void SaveHeightmap(string outputPath, float[,] heightmap, float min, float max)
        {
            var bitmap = new Bitmap(heightmap.GetLength(1), heightmap.GetLength(0), PixelFormat.Format24bppRgb);
            for (int y = 0; y < heightmap.GetLength(0); y++)
            {
                for (int x = 0; x < heightmap.GetLength(1); x++)
                {
                    var normalisedHeight = (int)Utility.Normalise(heightmap[y, x], min, max, 0, 255);
                    var pixel = Color.FromArgb(normalisedHeight, normalisedHeight, normalisedHeight);
                    bitmap.SetPixel(x, y, pixel);
                }
            }

            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            bitmap.Save($"{outputPath}.bmp", ImageFormat.Bmp);
            bitmap.Dispose();
        }
    }

    internal struct Rgb
    {
        public byte r { get; set; }
        public byte g { get; set; }
        public byte b { get; set; }
    }

    internal class RawImageParams
    {
        public int width { get; set; }

        public int height { get; set; }

        public int bitsPerPixel { get; set; }

        public int zeroOffset { get; set; }

        public int heightScaleMultiplier { get; set; }

        public RawImageParams()
        {
            bitsPerPixel = 16;
            heightScaleMultiplier = 8;
        }
    }
}
