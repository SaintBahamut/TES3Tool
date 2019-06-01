using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.ALCH;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class ALCH : Record
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
        /// Icon
        /// </summary>
        public TEXT TEXT { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Item properties
        /// </summary>
        public ALDT ALDT { get; set; }

        /// <summary>
        /// List of magical effects
        /// </summary>
        public List<ENAM> ENAM { get; set; }

        /// <summary>
        /// Script
        /// </summary>
        public SCRI SCRI { get; set; }

        public ALCH()
        {
        }

        public ALCH(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
