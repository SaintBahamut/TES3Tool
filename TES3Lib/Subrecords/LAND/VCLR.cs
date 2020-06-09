using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LAND
{
    /// <summary>
    /// Vertex colors
    /// </summary>
    public class VCLR : Subrecord
    {
        const int size = 65;

        public vcol[,] VertexColors { get; set; }

        public VCLR()
        {
        }

        public VCLR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            VertexColors = new vcol[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    var bytes = reader.ReadBytes<byte[]>(base.Data, 3);
                    VertexColors[y, x].r = bytes[0];
                    VertexColors[y, x].g = bytes[1];
                    VertexColors[y, x].b = bytes[2];
                }
            }
        }
    }

    public struct vcol
    {
        public byte r;
        public byte g;
        public byte b;
    }
}