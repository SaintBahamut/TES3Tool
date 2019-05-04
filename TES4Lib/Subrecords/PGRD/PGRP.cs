using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.PGRD
{
    public class PGRP : Subrecord
    {
        public GridNode[] Points { get; set; }

        public PGRP(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            int nodeCount = base.Data.Length / 16;
            Points = new GridNode[nodeCount];

            for (int i = 0; i < nodeCount; i++)
            {
                Points[i].x = reader.ReadBytes<float>(base.Data);
                Points[i].y = reader.ReadBytes<float>(base.Data);
                Points[i].z = reader.ReadBytes<float>(base.Data);
                Points[i].type = reader.ReadBytes<byte>(base.Data);
                Points[i].filter = reader.ReadBytes<byte[]>(base.Data,3);
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
