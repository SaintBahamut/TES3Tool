using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.CELL
{
    public class NAM0 : Subrecord
    {
        public int ObjectCount { get; set; }

        public NAM0(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ObjectCount = reader.ReadBytes<int>(base.Data);
        }
    }
}
