using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.INFO
{
    /// <summary>
    /// Info name string (unique sequence of #'s), ID
    /// </summary>
    [DebuggerDisplay("{InfoName}")]
    public class INAM : Subrecord
    {
        public string InfoName { get; set; }

        public INAM()
        {
        }

        public INAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            InfoName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
