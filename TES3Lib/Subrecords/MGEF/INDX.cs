using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    public class INDX : Subrecord
    {
        public MagicEffectId EffectId { get; set; }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EffectId = (MagicEffectId)reader.ReadBytes<int>(base.Data);
        }
    }
}