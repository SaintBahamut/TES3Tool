using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class XCHG : Subrecord
    {
        public float EnchantCharge { get; set; }

        public XCHG()
        {
        }

        public XCHG(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EnchantCharge = reader.ReadBytes<float>(base.Data);
        }
    }
}
