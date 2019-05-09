using System.Diagnostics;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// FormId of referenced AI Package
    /// </summary>
    [DebuggerDisplay("{AiPackage}")]
    public class PKID : Subrecord
    {
        public string AiPackage { get; set; }

        public PKID(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            AiPackage = reader.ReadFormId(base.Data);
        }
    }
}
