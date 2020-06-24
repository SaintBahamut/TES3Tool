using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    LowLodHeightMap[y, x] = reader.ReadBytes<byte>(base.Data);
                }
            }
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            for (int y = 0; y < LowLodHeightMap.GetLength(0); y++)
            {
                for (int x = 0; x < LowLodHeightMap.GetLength(1); x++)
                {
                    data.AddRange(ByteWriter.ToBytes(LowLodHeightMap[y, x], typeof(byte)));
                }
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}