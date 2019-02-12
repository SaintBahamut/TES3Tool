using Utility;

namespace TES4Lib.Structures.Base
{
    public class Subrecord
    {
        public string Name { get; set; }
        public short Size { get; set; }
        public byte[] Data { get; set; }
        public byte[] RawData { get; set; }

        public Subrecord(byte[] rawData)
        {
            RawData = rawData;
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(RawData, 4);
            Size = reader.ReadBytes<short>(RawData);
            Data = reader.ReadBytes<byte[]>(RawData, (int)Size);
        }
    }
}
