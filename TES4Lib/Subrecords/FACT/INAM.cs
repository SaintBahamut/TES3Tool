using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.FACT
{
    /// <summary>
    /// Rank insignia graphic path
    /// </summary>
    public class INAM : Subrecord
    {
        public string RankInsigniaImage { get; set; }

        public INAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            RankInsigniaImage = reader.ReadBytes<string>(base.Data, base.Size);        
        }
    }
}
