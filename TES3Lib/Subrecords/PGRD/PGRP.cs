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
        public Point[] Points { get; set; }

        public PGRP()
        {

        }

        public PGRP(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            int nodeCount = base.Data.Length / 16;
            Points = new Point[nodeCount];

            for (int i = 0; i < nodeCount; i++)
            {
                Points[i].X = reader.ReadBytes<int>(base.Data);
                Points[i].Y = reader.ReadBytes<int>(base.Data);
                Points[i].Z = reader.ReadBytes<int>(base.Data);
                Points[i].IsUserPoint = reader.ReadBytes<byte>(base.Data);
                Points[i].ConnectionsCount = reader.ReadBytes<byte>(base.Data);
                Points[i].Unknown1 = reader.ReadBytes<byte>(base.Data);
                Points[i].Unknown2 = reader.ReadBytes<byte>(base.Data);
            }
            
        }

        public struct Point
        {
            public int X;
            public int Y;
            public int Z;
            public byte IsUserPoint;
            public byte ConnectionsCount;
            public byte Unknown1;
            public byte Unknown2;
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();
            foreach (Point gridNode in Points)
            {
                data.AddRange(ByteWriter.ToBytes(gridNode.X, typeof(int)));
                data.AddRange(ByteWriter.ToBytes(gridNode.Y, typeof(int)));
                data.AddRange(ByteWriter.ToBytes(gridNode.Z, typeof(int)));
                data.Add(gridNode.IsUserPoint);
                data.Add(gridNode.ConnectionsCount);
                data.Add(gridNode.Unknown1);
                data.Add(gridNode.Unknown2);
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}
