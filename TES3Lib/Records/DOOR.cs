using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Door Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class DOOR : Record
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
        /// Script
        /// </summary>
        public SCRI SCRI { get; set; }

        /// <summary>
        /// Door open sound
        /// </summary>
        public SNAM SNAM { get; set; }

        /// <summary>
        /// Door close sound
        /// </summary>
        public ANAM ANAM { get; set; }

        public DOOR()
        {
        }

        public DOOR(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
