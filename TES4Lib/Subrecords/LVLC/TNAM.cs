using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.LVLC
{
    public class TNAM : Subrecord
    {
        public string CreatureTemplateFormId { get; set; }

        public TNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CreatureTemplateFormId = reader.ReadFormId(base.Data);
        }
    }
}
