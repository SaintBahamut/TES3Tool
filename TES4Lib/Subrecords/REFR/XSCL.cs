using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class XSCL : Subrecord
    {
        public float Scale { get; set; }

        public XSCL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Scale = reader.ReadBytes<float>(base.Data);
        }
    }
}