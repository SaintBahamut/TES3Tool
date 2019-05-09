using System.Diagnostics;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// FormId of referenced CSTY record
    /// </summary>
    [DebuggerDisplay("CombatStyle: {CombatStyleFormId}")]
    public class ZNAM : Subrecord
    {
        public string CombatStyleFormId { get; set; }

        public ZNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CombatStyleFormId = reader.ReadFormId(base.Data);
        }
    }
}
