using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature sound data: sound reference
    /// </summary>
    public class CSDI : Subrecord
    {
        /// <summary>
        /// FormId referencing a SOUN record
        /// </summary>
        public string SoundFormFormId { get; set; }

        public CSDI(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundFormFormId = reader.ReadFormId(base.Data);
        }
    }
}
