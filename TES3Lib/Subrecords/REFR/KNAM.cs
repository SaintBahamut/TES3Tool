using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class KNAM : Subrecord
    {
        public string DoorKeyId { get; set; }

        public KNAM()
        {

        }

        public KNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            DoorKeyId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
