using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// EditorId of reference or text data
    /// </summary>
    [DebuggerDisplay("{EditorId}")]
    public class ANAM : Subrecord
    {
        public string EditorId { get; set; }

        public ANAM()
        {
        }

        public ANAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
