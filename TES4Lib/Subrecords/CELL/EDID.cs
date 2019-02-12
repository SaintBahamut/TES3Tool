using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class EDID : Subrecord
    {
        public string CellEditorId { get; set; }

        public EDID(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CellEditorId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
