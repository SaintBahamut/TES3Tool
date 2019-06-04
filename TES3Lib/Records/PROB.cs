using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.PROB;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    [DebuggerDisplay("{NAME.EditorId}")]
    public class PROB : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public PBDT PBDT { get; set; }

        public ITEX ITEX { get; set; }

        public SCRI SCRI { get; set; }

        public PROB(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
