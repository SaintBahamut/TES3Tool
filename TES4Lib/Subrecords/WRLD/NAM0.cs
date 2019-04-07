using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.WRLD
{
    public class NAM0 : Subrecord
    {
        public int BottomLeftX { get; set; }

        public int BottomLeftY { get; set; }

        public NAM0(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            BottomLeftX = reader.ReadBytes<int>(base.Data);
            BottomLeftY = reader.ReadBytes<int>(base.Data);
        }
    }
}
