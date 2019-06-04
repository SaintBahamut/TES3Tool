using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.GLOB;
using FNAM = TES3Lib.Subrecords.GLOB.FNAM;
using static Utility.Common;
using System.Diagnostics;

namespace TES3Lib.Records
{
    /// <summary>
    /// Global variable record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class GLOB : Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Global type
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Float value
        /// </summary>
        public FLTV FLTV { get; set; }

        public GLOB()
        {
        }

        public GLOB(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
