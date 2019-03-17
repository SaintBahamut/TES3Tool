using Utility;

namespace TES4Lib.Base
{
    public class Subrecord
    {
        protected const int FORMID_LENGTH = 4;

        public string Name { get; set; }
        public ushort Size { get; set; }
        public byte[] Data { get; set; }
        public byte[] RawData { get; set; }

        public Subrecord(byte[] rawData)
        {
            RawData = rawData;
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(RawData, 4);
            Size = reader.ReadBytes<ushort>(RawData);
            Data = reader.ReadBytes<byte[]>(RawData, (int)Size);
        }
    }
}
