using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.FACT
{
    /// <summary>
    /// Faction interation with other faction
    /// </summary>
    public class XNAM : Subrecord
    {
        /// <summary>
        /// FormId of referenced faction
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// Disposition towards faction modifier
        /// </summary>
        public int Disposition { get; set; }

        public XNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            FormId = reader.ReadFormId(base.Data);
            Disposition = reader.ReadBytes<int>(base.Data);        
        }
    }
}
