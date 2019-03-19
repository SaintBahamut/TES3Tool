using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.ENCH
{
    /// <summary>
    /// Basic enchantment data
    /// Appears to hold 16 bytes of basic enchantment data.
    /// </summary>
    public class ENIT : Subrecord
    {
        public EnchantmentType EnchantmentType { get; set; }

        /// <summary>
        /// The manual charge value used if AutoCalc is off.
        /// </summary>
        public int ChargeAmmount { get; set; }

        /// <summary>
        /// The manual enchantment cost used if AutoCalc is off.
        /// </summary>
        public int EnchantCost { get; set; }

        /// <summary>
        /// Usually 0xCDCDCD00 or 0x00000000.
        /// 0x00000001 = Manual Enchant Cost(Autocalc Off)
        /// </summary>
        public int Flags { get; set; }

        public ENIT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EnchantmentType = (EnchantmentType)reader.ReadBytes<int>(base.Data);
            ChargeAmmount = reader.ReadBytes<int>(base.Data);
            EnchantCost = reader.ReadBytes<int>(base.Data);
            Flags = reader.ReadBytes<int>(base.Data);
        }
    }
}
