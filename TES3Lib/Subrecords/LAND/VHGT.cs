using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LAND
{
    /// <summary>
    /// Vertex height map data
    /// </summary>
    public class VHGT : Subrecord
    {
        const int size = 65;

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
        public byte[,] HeightDelta { get; set; }

        public short Unknown { get; set; }

        public VHGT()
        {
        }

        public VHGT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            HeightOffset = reader.ReadBytes<float>(base.Data);

            HeightDelta = new byte[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    HeightDelta[x,y] = reader.ReadBytes<byte>(base.Data, 3);
                }
            }

            Unknown = reader.ReadBytes<short>(base.Data);
        }
    }
}