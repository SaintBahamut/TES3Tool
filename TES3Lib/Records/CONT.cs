using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.CONT;
using System.Collections.Generic;
using Utility;
using System;
using static Utility.Common;
using System.Diagnostics;

namespace TES3Lib.Records
{
    /// <summary>
    /// Container Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class CONT : Record
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
        /// Container properties
        /// </summary>
        public CNDT CNDT { get; set; }

        /// <summary>
        /// Flags
        /// </summary>
        public FLAG FLAG { get; set; }

        /// <summary>
        /// Script
        /// </summary>
        public SCRI SCRI { get; set; }

        /// <summary>
        /// Item list
        /// </summary>
        public List<NPCO> NPCO { get; set; }

        public CONT()
        {
        }

        public CONT(byte[] rawData) : base(rawData)
        {
            NPCO = new List<NPCO>();
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
