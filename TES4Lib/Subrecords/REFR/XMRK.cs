using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class XMRK : Subrecord
    {
        public byte[] MapMarker { get; set; }

        public XMRK(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MapMarker = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
