using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Item dropped on death
    /// </summary>
    public class INAM : Subrecord
    {
        /// <summary>
        /// FormId referencing LVLI record
        /// </summary>
        public string ItemFormId { get; set; }

        public INAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ItemFormId = reader.ReadFormId(base.Data);
        }
    }
}
