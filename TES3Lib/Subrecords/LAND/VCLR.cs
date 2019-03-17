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
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    var bytes = reader.ReadBytes<byte[]>(base.Data, 3);
                    VertexColors[x, y].r = bytes[0];
                    VertexColors[x, y].g = bytes[1];
                    VertexColors[x, y].b = bytes[2];
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