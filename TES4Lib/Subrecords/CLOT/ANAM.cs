using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CLOT
{
    /// <summary>
    /// Enchantment Points
    /// </summary>
    public class ANAM : Subrecord
    {
        /// <summary>
        /// Enchantment value
        /// </summary>
        public short EnchantmentPoints { get; set; }

        public ANAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EnchantmentPoints = reader.ReadBytes<short>(base.Data);
        }
    }
}
