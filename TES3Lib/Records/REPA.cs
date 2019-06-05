using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.REPA;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Repair item record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class REPA : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public RIDT RIDT { get; set; }

        public ITEX ITEX { get; set; }

        public SCRI SCRI { get; set; }

        public REPA(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
