using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.INFO
{
    /// <summary>
    /// Next info ID (form a linked list of INFOs for the DIAL).
    /// First INFO has an empty PNAM, last has an empty NNAM.
    /// </summary>
    [DebuggerDisplay("{NextInfoId}")]
    public class NNAM : Subrecord
    {
        public string NextInfoId { get; set; }

        public NNAM()
        {
        }

        public NNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            NextInfoId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
