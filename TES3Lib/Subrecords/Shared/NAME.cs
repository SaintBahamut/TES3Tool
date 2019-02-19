using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    public class NAME : Subrecord
    {
        public string EditorId { get; set; }

        public NAME()
        {

        }

        public NAME(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            EditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
