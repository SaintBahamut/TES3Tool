using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    public class HVFX : Subrecord
    {
        public string HitVisual { get; set; }

        public HVFX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            HitVisual = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}