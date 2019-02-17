using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class XCNT : Subrecord
    {
        public int Count { get; set; }

        public XCNT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Count = reader.ReadBytes<int>(base.Data);
        }
    }
}
