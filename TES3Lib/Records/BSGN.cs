using System.Collections.Generic;
using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.BSGN;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Birthsign Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class BSGN : Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Birthsign graphic
        /// </summary>
        public TNAM TNAM { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public DESC DESC { get; set; }

        /// <summary>
        /// Powers
        /// </summary>
        public List<NPCS> NPCS { get; set; }

        public BSGN()
        {
        }

        public BSGN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
