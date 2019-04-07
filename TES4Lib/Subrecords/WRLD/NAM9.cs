using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.WRLD
{
    public class NAM9 : Subrecord
    {
        public int TopRightX { get; set; }

        public int TopRightY { get; set; }

        public NAM9(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            TopRightX = reader.ReadBytes<int>(base.Data);
            TopRightY = reader.ReadBytes<int>(base.Data);
        }
    }
}
