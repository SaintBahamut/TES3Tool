using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class XRNK : Subrecord
    {
        public int FactionRank { get; set; }

        public XRNK(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FactionRank = reader.ReadBytes<int>(base.Data);
        }
    }
}
