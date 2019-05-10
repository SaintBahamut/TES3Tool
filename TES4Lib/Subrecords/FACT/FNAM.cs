using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.FACT
{
    /// <summary>
    /// Female rank title
    /// </summary>
    public class FNAM : Subrecord
    {
        public string FemaleRankTitle { get; set; }

        public FNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            FemaleRankTitle = reader.ReadBytes<string>(base.Data, base.Size);        
        }
    }
}
