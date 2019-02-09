using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class FRMR : Subrecord
    {
        public int ObjectIndex { get; set; }

        public FRMR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ObjectIndex = reader.ReadBytes<int>(base.Data);
        }
    }
}
