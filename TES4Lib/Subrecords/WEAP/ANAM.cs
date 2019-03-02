using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.WEAP
{
    /// <summary>
    /// Enchantment Points
    /// </summary>
    public class ANAM : Subrecord
    {
        /// <summary>
        /// Enchantment value
        /// </summary>
        public short Points { get; set; }

        public ANAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Points = reader.ReadBytes<short>(base.Data);
        }
    }
}
