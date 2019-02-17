using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class UNAM : Subrecord
    {
        public byte ReferenceBlocked { get; set; }

        public UNAM()
        {

        }

        public UNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ReferenceBlocked = reader.ReadBytes<byte[]>(base.Data, base.Size)[0];
        }
    }
}
