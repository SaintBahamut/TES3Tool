using TES3Lib.Base;
using TES3Lib.Subrecords.SNDG;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;
using System.Diagnostics;

namespace TES3Lib.Records
{
    [DebuggerDisplay("{NAME.EditorId}")]
    public class SNDG : Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Sound type
        /// </summary>
        public DATA DATA { get; set; }

        /// <summary>
        /// Sound EditorId
        /// </summary>
        public SNAM SNAM { get; set; }

        /// <summary>
        /// Creature name (optional)
        /// </summary>
        public CNAM CNAM { get; set; }

        public SNDG(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
