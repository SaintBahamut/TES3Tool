using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class NAME: Subrecord
    {
        public string ObjectId { get; set; }

        public NAME()
        {

        }

        public NAME(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ObjectId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
