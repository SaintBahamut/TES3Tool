using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LEVC
{
    /// <summary>
    /// EditorId of leveled creature
    /// </summary>
    public class CNAM : Subrecord
    {
        /// <summary>
        /// Id of creature
        /// </summary>
        public string CreatureEditorId { get; set; }

        public CNAM()
        {
        }

        public CNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CreatureEditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
