using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature blood decal
    /// </summary>
    public class NAM1 : Subrecord
    {
        /// <summary>
        /// Blood decal path
        /// </summary>
        public string BloodDecalPath { get; set; }

        public NAM1(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            BloodDecalPath = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
