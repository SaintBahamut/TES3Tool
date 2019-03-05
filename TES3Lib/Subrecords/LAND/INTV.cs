using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LAND
{
    /// <summary>
    /// The cell coordinates of the cell
    /// </summary>
    public class INTV : Subrecord
    {
        public int CellX { get; set; }

        public int CellY { get; set; }

        public INTV()
        {
        }

        public INTV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CellX = reader.ReadBytes<int>(base.Data);
            CellY = reader.ReadBytes<int>(base.Data);
        }
    }
}