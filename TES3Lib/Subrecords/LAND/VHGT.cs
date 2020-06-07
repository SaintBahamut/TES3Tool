using System;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LAND
{
    /// <summary>
    /// Vertex height map data
    /// </summary>
    public class VHGT : Subrecord
    {
        public const int CELL_SIDE = 65;
        public const int CELL_SIZE = CELL_SIDE* CELL_SIDE;


        /// <summary>
        /// A height offset for the entire cell.
        /// Decreasing this value will shift the
        /// entire cell land down
        /// </summary>
        public float HeightOffset { get; set; }

        /// <summary>
        /// Contains the height data for the cell in the form of a 65x65 pixel array. The
        /// height data is not absolute values but uses differences between adjacent pixels.
        /// Thus a pixel value of 0 means it has the same height as the last pixel.Note that
        /// the y-direction of the data is from the bottom up.
        /// </summary>
        public sbyte[,] HeightDelta { get; set; }

        public sbyte[] HeightVector { get; set; }

        public short Unknown1 { get; set; }

        public byte Unknown2 { get; set; }

        public VHGT()
        {
        }

        public VHGT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            HeightOffset = reader.ReadBytes<float>(base.Data);
            HeightDelta = new sbyte[CELL_SIDE, CELL_SIDE];
            for (int y = 0; y < CELL_SIDE; y++)
            {
                for (int x = 0; x < CELL_SIDE; x++)
                {
                    HeightDelta[y,x] = reader.ReadBytes<sbyte>(base.Data);
                }
            }

            Unknown1 = reader.ReadBytes<short>(base.Data);
            Unknown2 = reader.ReadBytes<byte>(base.Data);
        }
    }
}