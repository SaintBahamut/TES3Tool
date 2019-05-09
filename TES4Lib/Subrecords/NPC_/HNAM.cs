using System.Diagnostics;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// FormId of referenced HAIR record
    /// </summary>
    [DebuggerDisplay("{HairFormId}")]
    public class HNAM : Subrecord
    {
        public string HairFormId { get; set; }

        public HNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            HairFormId = reader.ReadFormId(base.Data);
        }
    }
}
