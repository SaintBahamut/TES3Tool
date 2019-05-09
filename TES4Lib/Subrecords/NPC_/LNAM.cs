using System.Diagnostics;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// Hair length
    /// </summary>
    [DebuggerDisplay("HairLength: {HairLength}")]
    public class LNAM : Subrecord
    {
        public float HairLength { get; set; }

        public LNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            HairLength = reader.ReadBytes<float>(base.Data);
        }
    }
}
