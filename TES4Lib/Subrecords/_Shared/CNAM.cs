using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// FormId of record reference
    /// </summary>
    public class CNAM : Subrecord
    {
        public string FormId { get; set; }

        public CNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FormId = reader.ReadFormId(base.Data);
        }
    }
}
