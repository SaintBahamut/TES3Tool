using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.LIGH
{
    public class EDID : Subrecord
    {
        public string RecordEditorId { get; set; }

        public EDID(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            RecordEditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
