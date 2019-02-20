using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MISC
{
    /// <summary>
    /// Enchantment ID string
    /// </summary>
    public class ENAM : Subrecord
    {
        public string EnchantmentId { get; set; }

        public ENAM()
        {

        }

        public ENAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EnchantmentId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
