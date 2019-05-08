using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// EditorId of reference or text data
    /// </summary>
    [DebuggerDisplay("{EditorId}")]
    public class BNAM : Subrecord
    {
        public string EditorId { get; set; }

        public BNAM()
        {

        }

        public BNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
