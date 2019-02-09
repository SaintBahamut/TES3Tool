using Utility;

namespace TES4Lib.Structures.Base
{
    public class Record
    {
        public string Name { get; set; }
        public ulong Size { get; set; }
        public ulong Flag { get; set; }
        public byte[] FormId { get; set; }
        public ulong VersionControlInfo { get; set; }
        public byte[] Data { get; set; }
        private byte[] RawData { get; set; }

        public Record(byte[] rawData)
        {
            RawData = rawData;
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(RawData, 4);
            Size = reader.ReadBytes<ulong>(RawData);
            Flag = reader.ReadBytes<ulong>(RawData, 4);
            FormId = reader.ReadBytes<byte[]>(RawData, 8);
            Data = reader.ReadBytes<byte[]>(RawData,(int)Size);
        }
    }
}
