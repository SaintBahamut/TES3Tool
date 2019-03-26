using System;
using System.Linq;
using Utility;

namespace TES4Lib.Base
{
    public class Subrecord
    {
        protected const int FORMID_LENGTH = 4;

        protected string Name { get; set; }
        protected ushort Size { get; set; }
        protected byte[] Data { get; set; }
        protected byte[] RawData { get; set; }

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
