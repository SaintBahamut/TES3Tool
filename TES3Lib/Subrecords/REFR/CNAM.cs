using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    /// <summary>
    /// Faction of reference
    /// </summary>
    public class CNAM : Subrecord
    {
        public string FactionOwnerId { get; set; }

        public CNAM()
        {
        }

        public CNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FactionOwnerId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
