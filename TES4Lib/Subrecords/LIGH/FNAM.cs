using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.LIGH
{
    public class FNAM : Subrecord
    {
        public float FadeValue { get; set; }

        public FNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FadeValue = reader.ReadBytes<float>(base.Data);
        }
    }
}
