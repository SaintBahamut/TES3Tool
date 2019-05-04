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
                Points[i].X = reader.ReadBytes<float>(base.Data);
                Points[i].Y = reader.ReadBytes<float>(base.Data);
                Points[i].Z = reader.ReadBytes<float>(base.Data);
                Points[i].EdgeCount = reader.ReadBytes<byte>(base.Data);
                Points[i].PointType = reader.ReadBytes<byte[]>(base.Data,3);
            }
            
        }

        public struct GridNode
        {
            public float X;
            public float Y;
            public float Z;
            public byte EdgeCount;
            public byte[] PointType;
        }
    }
}
