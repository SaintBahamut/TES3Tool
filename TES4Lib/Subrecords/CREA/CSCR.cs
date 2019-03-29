using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature inherits sounds from other creature
    /// </summary>
    public class CSCR : Subrecord
    {
        /// <summary>
        /// FormId referencing a CREA record
        /// </summary>
        public string SoundInheritFormId { get; set; }

        public CSCR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundInheritFormId = reader.ReadFormId(base.Data);
        }
    }
}
