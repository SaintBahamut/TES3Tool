using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.BOOK;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Books Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class BOOK : Record
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
        /// Book properties
        /// </summary>
        public BKDT BKDT { get; set; }

        /// <summary>
        /// Script
        /// </summary>
        public SCRI SCRI { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public ITEX ITEX { get; set; }

        /// <summary>
        /// Books text
        /// </summary>
        public TEXT TEXT { get; set; }

        /// <summary>
        /// Enhancment
        /// </summary>
        public ENAM ENAM { get; set; }

        public BOOK()
        {
        }

        public BOOK(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
