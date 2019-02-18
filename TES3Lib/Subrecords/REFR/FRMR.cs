using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class FRMR : Subrecord
    {
        public int ObjectIndex { get; set; }

        public FRMR()
        {

        }

        public FRMR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ObjectIndex = reader.ReadBytes<int>(base.Data);
        }
    }
}
