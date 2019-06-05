using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.LTEX;
using static Utility.Common;
using System.Diagnostics;

namespace TES3Lib.Records
{
    /// <summary>
    /// Land texture Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class LTEX : Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Counter
        /// </summary>
        public INTV INTV { get; set; }

        /// <summary>
        /// Texture
        /// </summary>
        public DATA DATA { get; set; }

        public LTEX(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
