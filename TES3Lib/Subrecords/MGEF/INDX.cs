using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;
using Utility.Attributes;

namespace TES3Lib.Subrecords.MGEF
{
    public class INDX : Subrecord
    {
        public MagicEffect EffectId { get; set; }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EffectId = reader.ReadBytes<MagicEffect>(base.Data);
        }
    }
}