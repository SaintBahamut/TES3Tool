using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;
using Utility.Attributes;

namespace TES3Lib.Subrecords.MGEF
{
    public class INDX : Subrecord
    {
        [SizeInBytes(4)]
        public MagicEffect EffectId { get; set; }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EffectId = (MagicEffect)reader.ReadBytes<int>(base.Data);
        }
    }
}