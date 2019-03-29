using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature turn speed
    /// </summary>
    public class TNAM : Subrecord
    {
        /// <summary>
        /// Turn speed value
        /// </summary>
        public float TurningSpeed { get; set; }

        public TNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            TurningSpeed = reader.ReadBytes<float>(base.Data);
        }
    }
}
