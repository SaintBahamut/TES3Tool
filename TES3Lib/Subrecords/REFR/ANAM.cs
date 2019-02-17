using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class ANAM : Subrecord
    {
        public string OwnerId { get; set; }

        public ANAM()
        {

        }

        public ANAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            OwnerId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
