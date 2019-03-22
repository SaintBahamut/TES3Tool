using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.ENCH
{
    /// <summary>
    /// Alchemy data
    /// </summary>
    public class ENDT : Subrecord
    {
        public EnchantmentType Type { get; set; }

        public int EnchantCost { get; set; }

        public int Charge { get; set; }

        /// <summary>
        /// Possible values 0 or 1
        /// </summary>
        public int AutoCalc { get; set; } //?

        public ENDT()
        {
        }

        public ENDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = reader.ReadBytes<EnchantmentType>(base.Data);
            EnchantCost = reader.ReadBytes<int>(base.Data);
            Charge = reader.ReadBytes<int>(base.Data);
            AutoCalc = reader.ReadBytes<int>(base.Data);
        }
    }
}
