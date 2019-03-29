using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature base scale
    /// </summary>
    public class BNAM : Subrecord
    {
        /// <summary>
        /// Scale value
        /// </summary>
        public float BaseScale { get; set; }

        public BNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            BaseScale = reader.ReadBytes<float>(base.Data);
        }
    }
}
