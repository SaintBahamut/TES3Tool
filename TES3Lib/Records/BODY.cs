using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.BODY;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Body Part Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class BODY : Record
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
        /// Race EditorId
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Body part data and flags
        /// </summary>
        public BYDT BYDT { get; set; }

        public BODY()
        {
            NAME = new NAME();
            MODL = new MODL() { ModelPath = "\0" };
            FNAM = new FNAM() { Name = "Argonian\0" };
            BYDT = new BYDT();
        }

        public BODY(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
