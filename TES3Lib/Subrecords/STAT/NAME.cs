using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.STAT
{
    public class NAME : Subrecord
    {
        /// <summary>
        /// Editor Id of STAT
        /// </summary>
        public string EditorId { get; set; }

        public NAME(string editorId)
        {
            EditorId = editorId;
        }

        public NAME(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
