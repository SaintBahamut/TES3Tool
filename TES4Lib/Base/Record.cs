using Utility;

namespace TES4Lib.Structures.Base
{
    public class Record
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int Flag { get; set; }
        public byte[] FormId { get; set; }
        public int VersionControlInfo { get; set; }
        public byte[] Data { get; set; }
        private byte[] RawData { get; set; }

        public Record(byte[] rawData)
        {
            RawData = rawData;
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(RawData, 4);
            Size = reader.ReadBytes<int>(RawData);
            Flag = reader.ReadBytes<int>(RawData, 4);
            FormId = reader.ReadBytes<byte[]>(RawData, 8);
            Data = reader.ReadBytes<byte[]>(RawData,(int)Size);
        }
    }
}
