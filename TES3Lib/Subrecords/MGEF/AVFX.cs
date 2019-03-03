using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    public class AVFX : Subrecord
    {
        public string AreaVisual { get; set; }

        public AVFX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            AreaVisual = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}