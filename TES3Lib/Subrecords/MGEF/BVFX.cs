using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    public class BVFX : Subrecord
    {
        public string BoltVisual { get; set; }

        public BVFX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            BoltVisual = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}