using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.WRLD
{
    public class MNAM : Subrecord
    {
        public int AreaDimX { get; set; }

        public int AreaDimY { get; set; }

        public short NWCellX { get; set; }

        public short NWCellY { get; set; }

        public short SECellX { get; set; }

        public short SECellY { get; set; }


        public MNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            AreaDimX = reader.ReadBytes<int>(base.Data);
            AreaDimY = reader.ReadBytes<int>(base.Data);
            NWCellX = reader.ReadBytes<short>(base.Data);
            NWCellY = reader.ReadBytes<short>(base.Data);
            SECellX = reader.ReadBytes<short>(base.Data);
            SECellY = reader.ReadBytes<short>(base.Data);
        }
    }
}
