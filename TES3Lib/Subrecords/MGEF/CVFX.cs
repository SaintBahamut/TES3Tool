using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    public class CVFX : Subrecord
    {
        public string CastingVisual { get; set; }

        public CVFX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CastingVisual = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
