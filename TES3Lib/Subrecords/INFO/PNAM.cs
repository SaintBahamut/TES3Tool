using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.INFO
{
    /// <summary>
    /// Previous info ID
    /// </summary>
    [DebuggerDisplay("{PreviousInfoId}")]
    public class PNAM : Subrecord
    {
        public string PreviousInfoId { get; set; }

        public PNAM()
        {
        }

        public PNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            PreviousInfoId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
