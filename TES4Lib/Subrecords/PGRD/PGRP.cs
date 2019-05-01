using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.PGRD
{
    public class PGRP : Subrecord
    {
        public GridNode[] GridNodes { get; set; }

        public PGRP(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            int nodeCount = base.Data.Length / 16;
            GridNodes = new GridNode[nodeCount];

            for (int i = 0; i < nodeCount; i++)
            {
                GridNodes[i].x = reader.ReadBytes<float>(base.Data);
                GridNodes[i].y = reader.ReadBytes<float>(base.Data);
                GridNodes[i].z = reader.ReadBytes<float>(base.Data);
                GridNodes[i].type = reader.ReadBytes<byte>(base.Data);
                GridNodes[i].filter = reader.ReadBytes<byte[]>(base.Data,3);
            }
            
        }

        public struct GridNode
        {
            public float x;
            public float y;
            public float z;
            public byte type;
            public byte[] filter;
        }
    }
}
