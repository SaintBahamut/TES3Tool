using System.Diagnostics;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Base editor id
    /// </summary>
    [DebuggerDisplay("{EditorId}")]
    public class EDID : Subrecord
    {
        /// <summary>
        /// Editor name of record
        /// </summary>
        public string EditorId { get; set; }

        public EDID(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
