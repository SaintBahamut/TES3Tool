using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.FACT
{
    /// <summary>
    /// Rank number
    /// </summary>
    public class RNAM : Subrecord
    {
        public int Rank { get; set; }

        public RNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            Rank = reader.ReadBytes<int>(base.Data);        
        }
    }
}
