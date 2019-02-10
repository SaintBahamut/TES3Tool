using System;
using System.Collections.Generic;
using Utility;

namespace TES4Lib.Structures.Base
{
    public class Group
    {
        readonly public string Name;
        public int Size { get; set; }
        public string Label { get; set; }
        public int Type { get; set; }
        public int Stamp { get; set; }
        public byte[] Data { get; set; }
        private byte[] RawData { get; set; }

        private List<Record> records = new List<Record>();

        public List<Record> Records
        {
            get { return records; }
        }

        public Group SubGroup { get; set; }

        public Group(byte [] rawData)
        {
            RawData = rawData;
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(RawData, 4);
            Size = reader.ReadBytes<int>(RawData);
            Label = reader.ReadBytes<string>(RawData, 4);
            Type = reader.ReadBytes<int>(RawData);
            Stamp = reader.ReadBytes<int>(RawData);
            Data = reader.ReadBytes<byte[]>(RawData, RawData.Length - 20);
        }

        protected virtual void BuildRecords(ByteReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
