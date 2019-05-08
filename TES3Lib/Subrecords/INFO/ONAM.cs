using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.INFO
{
    /// <summary>
    /// Referenced actor Id
    /// </summary>
    [DebuggerDisplay("{ActorId}")]
    public class ONAM : Subrecord
    {
        public string ActorId { get; set; }

        public ONAM()
        {
        }

        public ONAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ActorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
