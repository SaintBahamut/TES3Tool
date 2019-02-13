using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class FNAM : Subrecord
    {
        public byte[] MapFlag { get; set; }

        public FNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MapFlag = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
