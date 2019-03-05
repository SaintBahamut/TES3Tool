using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LAND
{
    /// <summary>
    /// A 16x16 array of short texture indices (from a LTEX record?).
    /// </summary>
    public class VTEX : Subrecord
    {
        const int size = 16;

        public byte[,] TexIndices { get; set; }

        public VTEX()
        {
        }

        public VTEX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
    
            TexIndices = new byte[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    TexIndices[x, y] = reader.ReadBytes<byte>(base.Data, 3);
                }
            }
        }
    }
}