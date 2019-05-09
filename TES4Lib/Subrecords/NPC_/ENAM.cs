using System.Diagnostics;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// FormId of referenced EYES record
    /// </summary>
    [DebuggerDisplay("EyesFormId: {EyesFormId}")]
    public class ENAM : Subrecord
    {
        public string EyesFormId { get; set; }

        public ENAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EyesFormId = reader.ReadFormId(base.Data);
        }
    }
}
