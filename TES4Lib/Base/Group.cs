using Utility;

namespace TES4Lib.Structures.Base
{
    public class Group
    {
        public string Name { get; set; }
        public ulong Size { get; set; }
        public string Label { get; set; }
        public long Type { get; set; }
        public ulong Stamp { get; set; }
        public byte[] RawData { get; set; }

        public Group(byte [] rawData)
        {
            rawData = RawData;
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(RawData, 4);
            Size = reader.ReadBytes<ulong>(RawData);
            Label = reader.ReadBytes<string>(RawData, 4);
            Type = reader.ReadBytes<long>(RawData);
            Stamp = reader.ReadBytes<ulong>(RawData);
        }     
    }
}
