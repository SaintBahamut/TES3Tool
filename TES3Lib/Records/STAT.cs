using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Static object record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class STAT : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public STAT()
        {
        }

        public STAT(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
