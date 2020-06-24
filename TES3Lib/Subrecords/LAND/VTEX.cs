using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// 0 - is NOTEX,so in reality indices are +1 values from LTEX
        /// </summary>
        public ushort[,] TexIndices { get; set; }      

        public VTEX()
        {
        }

        public VTEX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            TexIndices = new ushort[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    TexIndices[y, x] = reader.ReadBytes<ushort>(base.Data);
                }
            }
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            for (int y = 0; y < TexIndices.GetLength(0); y++)
            {
                for (int x = 0; x < TexIndices.GetLength(1); x++)
                {
                    data.AddRange(ByteWriter.ToBytes(TexIndices[y, x], typeof(ushort)));
                }
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}