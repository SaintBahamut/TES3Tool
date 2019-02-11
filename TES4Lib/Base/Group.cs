using System;
using System.Collections.Generic;
using System.Reflection;
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

        private List<Group> groups = new List<Group>();

        public List<Group> Groups
        {
            get { return groups; }
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

        /// <summary>
        /// Builds Records or Groups
        /// </summary>
        protected virtual void BuildRecords()
        {
            if (Data.Length > 0) return;

            var reader = new ByteReader();
            var name = reader.ReadBytes<string>(Data, 4);
            var size = reader.ReadBytes<int>(Data);
            reader.offset = -8;

            if (!name.Equals("GROUP"))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Record record = assembly
                    .CreateInstance($"TES4Lib.Records.{name}", false, BindingFlags.Default, null, new object[] { reader.ReadBytes<byte[]>(Data, size) }, null, null) as Record;
                Records.Add(record);
            }
            else
            {
                Groups.Add(new Group(reader.ReadBytes<byte[]>(Data, size)));
            }
        }
    }
}
