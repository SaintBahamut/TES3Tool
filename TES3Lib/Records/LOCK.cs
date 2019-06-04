using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.LOCK;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Lockpicking items Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class LOCK : Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Model
        /// </summary>
        public MODL MODL { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Lockpicking item parameters
        /// </summary>
        public LKDT LKDT { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public ITEX ITEX { get; set; }

        /// <summary>
        /// Script
        /// </summary>
        public SCRI SCRI { get; set; }

        public LOCK(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
