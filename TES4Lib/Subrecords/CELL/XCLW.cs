using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XCLW : Subrecord
    {
        /// <summary>
        /// Water height (if not 0.00)
        /// </summary>
        public float WaterHeight { get; set; }

        public XCLW(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            WaterHeight = reader.ReadBytes<float>(base.Data);
        }
    }
}
