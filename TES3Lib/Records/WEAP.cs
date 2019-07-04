using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.WEAP;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Weapon record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class WEAP : Record
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
        /// Weapon properties
        /// </summary>
        public WPDT WPDT { get; set; }

        /// <summary>
        /// Script
        /// </summary>
        public SCRI SCRI { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public ITEX ITEX { get; set; }

        /// <summary>
        /// Enchantment
        /// </summary>
        public ENAM ENAM { get; set; }

        public WEAP()
        {
        }

        public WEAP(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
