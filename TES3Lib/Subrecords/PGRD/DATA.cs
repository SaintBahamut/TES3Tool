using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.PGRD
{
    public class DATA : Subrecord
    {
        /// <summary>
        /// GridX, 0 if interior
        /// </summary>
        public int GridX { get; set; }

        /// <summary>
        /// GridY, 0 if interior
        /// </summary>
        public int GridY { get; set; }

        /// <summary>
        /// Granularity of CS generated grid in cell
        /// </summary>
        public short Granularity { get; set; }

        /// <summary>
        /// Grid node count in cell
        /// </summary>
        public short Points { get; set; }

        public DATA()
        {

        }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            GridX = reader.ReadBytes<int>(base.Data);
            GridY = reader.ReadBytes<int>(base.Data);
            Granularity = reader.ReadBytes<short>(base.Data);
            Points = reader.ReadBytes<short>(base.Data);
        }
    }
}
