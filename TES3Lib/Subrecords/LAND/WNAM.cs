using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LAND
{
    /// <summary>
    /// low-LOD heightmap
    /// </summary>
    public class WNAM : Subrecord
    {
        public byte[,] LowLodHeightMap { get; set; }

        public WNAM()
        {
        }

        public WNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            LowLodHeightMap = new byte[9, 9];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    LowLodHeightMap[x, y] = reader.ReadBytes<byte>(base.Data, 3);
                }
            }
        }
    }
}