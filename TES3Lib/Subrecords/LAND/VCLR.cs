using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            for (int y = 0; y < VertexColors.GetLength(0); y++)
            {
                for (int x = 0; x < VertexColors.GetLength(1); x++)
                {
                    data.AddRange(ByteWriter.ToBytes(VertexColors[y, x].r, typeof(byte)));
                    data.AddRange(ByteWriter.ToBytes(VertexColors[y, x].g, typeof(byte)));
                    data.AddRange(ByteWriter.ToBytes(VertexColors[y, x].b, typeof(byte)));
                }
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }

    public struct vcol
    {
        public byte r;
        public byte g;
        public byte b;
    }
}