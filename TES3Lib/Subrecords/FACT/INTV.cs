using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.FACT
{
    /// <summary>
    /// Faction reaction value
    /// </summary>
    public class INTV : Subrecord
    {
        public int FactionReactionValue { get; set; }

        public INTV()
        {
        }

        public INTV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FactionReactionValue = reader.ReadBytes<int>(base.Data);
        }
    }
}
