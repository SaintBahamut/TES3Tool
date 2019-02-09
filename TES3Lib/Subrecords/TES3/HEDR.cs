using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.TES3
{
    internal class HEDR : Subrecord
    {

        public float Version { get; set; }

        public int Unknown { get; set; }

        //32 bytes
        public string CompanyName { get; set; }

        //256 bytes
        public string Description { get; set; }

        public int NumRecords { get; set; }

        public HEDR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Version = reader.ReadBytes<float>(base.Data);
            Unknown = reader.ReadBytes<int>(base.Data);
            CompanyName = reader.ReadBytes<string>(base.Data, 32);
            Description = reader.ReadBytes<string>(base.Data, 256);
            NumRecords = reader.ReadBytes<int>(base.Data);
        }
    }
}