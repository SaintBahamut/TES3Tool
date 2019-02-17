using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class FULL : Subrecord
    {
        public string CellName { get; set; }

        public FULL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CellName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
