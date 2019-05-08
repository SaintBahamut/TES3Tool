using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Attached script id
    /// </summary>
    [DebuggerDisplay("{ScriptName}")]
    public class SCPT : Subrecord
    {
        public string ScriptName { get; set; }

        public SCPT()
        {
        }

        public SCPT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ScriptName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
