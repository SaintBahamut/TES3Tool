using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.INGR;
using static Utility.Common;
using System.Diagnostics;

namespace TES3Lib.Records
{
    /// <summary>
    /// Ingredient Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class INGR : Record
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
        /// Ingredient parameters and effects
        /// </summary>
        public IRDT IRDT { get; set; }
        
        /// <summary>
        /// Icon
        /// </summary>
        public ITEX ITEX { get; set; }

        /// <summary>
        /// Script
        /// </summary>
        public SCRI SCRI { get; set; }

        public INGR()
        {
        }

        public INGR(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
