using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.CELL
{
    public class WHGT : Subrecord
    {
        public float WaterHeight { get; set; }

        public WHGT()
        {

        }

        public WHGT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            WaterHeight = reader.ReadBytes<float>(base.Data);
        }
    }
}
