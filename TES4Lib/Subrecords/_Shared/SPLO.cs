using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Actor spell
    /// </summary>
    public class SPLO : Subrecord
    {
        /// <summary>
        /// Spell reference
        /// </summary>
        public string SpellFormId { get; set; }

        public SPLO(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SpellFormId = reader.ReadFormId(base.Data);
        }
    }
}