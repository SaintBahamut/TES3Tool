using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.STAT
{
    public class EDID : Subrecord
    {
        public string EditorId { get; set; }

        public EDID(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
