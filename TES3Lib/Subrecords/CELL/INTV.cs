using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.CELL
{
    /// <summary>
    /// Water height in cell
    /// Some plugins (Morrowind.esm) use an INTV subrecord in CELL header instead of WHGT
    /// </summary>
    public class INTV : Subrecord
    {
        public int WaterHeight { get; set; }

        public INTV()
        {
        }

        public INTV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            WaterHeight = reader.ReadBytes<int>(base.Data);
        }       
    }
}