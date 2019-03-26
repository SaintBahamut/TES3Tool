using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Combat style
    /// </summary>
    public class ZNAM : Subrecord
    {
        /// <summary>
        /// FormId referencing a CSTY record
        /// </summary>
        public string CombatSyleFormId { get; set; }

        public ZNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CombatSyleFormId = reader.ReadFormId(base.Data);
        }
    }
}
