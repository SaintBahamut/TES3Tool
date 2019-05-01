using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.PGRD
{
    public class PGRP : Subrecord
    {
        public GridNode[] GridNodes { get; set; }

        public PGRP()
        {

        }

        public PGRP(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            int nodeCount = base.Data.Length / 16;
            GridNodes = new GridNode[nodeCount];

            for (int i = 0; i < nodeCount; i++)
            {
                GridNodes[i].x = reader.ReadBytes<int>(base.Data);
                GridNodes[i].y = reader.ReadBytes<int>(base.Data);
                GridNodes[i].z = reader.ReadBytes<int>(base.Data);
                GridNodes[i].type = reader.ReadBytes<int>(base.Data);
            }
            
        }

        public struct GridNode
        {
            public int x;
            public int y;
            public int z;
            public int type;
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();
            foreach (GridNode gridNode in GridNodes)
            {
                data.AddRange(ByteWriter.ToBytes(gridNode.x, typeof(int)));
                data.AddRange(ByteWriter.ToBytes(gridNode.y, typeof(int)));
                data.AddRange(ByteWriter.ToBytes(gridNode.z, typeof(int)));
                data.AddRange(ByteWriter.ToBytes(gridNode.type, typeof(int)));
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}
