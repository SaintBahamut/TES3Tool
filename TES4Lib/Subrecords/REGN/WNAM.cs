using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REGN
{
    /// <summary>
    /// World space FormId region belongs to
    /// </summary>
    public class WNAM : Subrecord
    {
        public string WorldSpaceFormId { get; set; }

        public WNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            WorldSpaceFormId = reader.ReadFormId(this.Data);
        }
    }
}
