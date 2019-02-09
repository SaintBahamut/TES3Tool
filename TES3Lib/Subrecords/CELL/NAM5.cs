using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.CELL
{
    public class NAM5 : Subrecord
    {
        public int MapColor { get; set; }

        public NAM5(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MapColor = reader.ReadBytes<int>(base.Data);
        }
    }
}
