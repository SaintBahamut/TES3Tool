using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.SOUN;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Sound record (mainly usable by other objects)
    /// FNAM == sound file path!
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class SOUN : Record
    {
        public NAME NAME { get; set; }

        /// <summary>
        /// Sound Filename (relative to Sounds\ dir)
        /// </summary>
        public FNAM FNAM { get; set; }

        public DATA DATA { get; set; }

        public SOUN()
        {

        }

        public SOUN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
