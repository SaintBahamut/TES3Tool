using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.MISC;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Misc item Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class MISC : Record
    {
        public NAME NAME { get; set; }
        public MODL MODL { get; set; }
        public FNAM FNAM { get; set; }
        public MCDT MCDT { get; set; }
        public SCRI SCRI { get; set; }
        public ITEX ITEX { get; set; }
        public ENAM ENAM { get; set; }

        public MISC()
        {

        }

        public MISC(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
