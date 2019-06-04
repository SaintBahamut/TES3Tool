using System.Collections.Generic;
using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.ENCH;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Enchantment Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class ENCH : Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Enchantment properties
        /// </summary>
        public ENDT ENDT { get; set; }

        /// <summary>
        /// Effects
        /// </summary>
        public List<ENAM> ENAM { get; set; }

        public ENCH()
        {
        }

        public ENCH(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
