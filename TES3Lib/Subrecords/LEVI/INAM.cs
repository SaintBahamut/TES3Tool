using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LEVI
{
    /// <summary>
    /// EditorId of leveled item
    /// </summary>
    public class INAM : Subrecord
    {
        /// <summary>
        /// Id of item
        /// </summary>
        public string ItemEditorId { get; set; }

        public INAM()
        {
        }

        public INAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ItemEditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
