using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.PGRD
{
    public class PGRC : Subrecord
    {
        public int[] NodeLinks { get; set; }

        public PGRC()
        {

        }

        public PGRC(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            NodeLinks = new int[base.Data.Length/sizeof(int)];


            for (int i = 0; i < NodeLinks.GetLength(0); i++)
            {
                NodeLinks[i] = reader.ReadBytes<int>(base.Data);
            }
            
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            for (int i = 0; i < NodeLinks.GetLength(0); i++)
            {
                data.AddRange(ByteWriter.ToBytes(NodeLinks[i], typeof(int)));
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}
