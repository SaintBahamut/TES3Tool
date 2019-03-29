using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature blood spray
    /// </summary>
    public class NAM0 : Subrecord
    {
        /// <summary>
        /// Blood spray path
        /// </summary>
        public string BloodSprayPath { get; set; }

        public NAM0(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            BloodSprayPath = reader.ReadBytes<string>(base.Data,base.Size);
        }
    }
}
