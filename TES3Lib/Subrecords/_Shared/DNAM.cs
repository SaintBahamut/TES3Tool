using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// EditorId of reference
    /// </summary>
    [DebuggerDisplay("{EditorId}")]
    public class DNAM : Subrecord
    {
        public string EditorId { get; set; }

        public DNAM()
        {
        }

        public DNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}