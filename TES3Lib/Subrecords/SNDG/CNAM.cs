using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.SNDG
{
    /// <summary>
    /// Female part name
    /// </summary>
    [DebuggerDisplay("{CreatureName}")]
    public class CNAM : Subrecord
    {
        /// <summary>
        /// Female tagged bodpart id
        /// </summary>
        public string CreatureName { get; set; }

        public CNAM()
        {

        }

        public CNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CreatureName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
