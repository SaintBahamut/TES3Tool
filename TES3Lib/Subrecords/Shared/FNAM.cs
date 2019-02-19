using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    public class FNAM : Subrecord
    {
        public string Name { get; set; }

        public FNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}