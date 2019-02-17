using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class FULL : Subrecord
    {
        public string FullName { get; set; }

        public FULL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FullName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
