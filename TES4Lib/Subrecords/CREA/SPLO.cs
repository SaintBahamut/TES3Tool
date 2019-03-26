using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature spell
    /// </summary>
    public class SPLO : Subrecord
    {
        /// <summary>
        /// points to a SPEL or LVSP record
        /// </summary>
        public string SpellFormId { get; set; }

        public SPLO(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SpellFormId = reader.ReadFormId(base.Data);
        }
    }
}
