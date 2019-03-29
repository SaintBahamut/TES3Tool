using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature foot weight
    /// </summary>
    public class WNAM : Subrecord
    {
        /// <summary>
        /// Food weight value
        /// </summary>
        public float FootWeight { get; set; }

        public WNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FootWeight = reader.ReadBytes<float>(base.Data);
        }
    }
}
