using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class FLTV : Subrecord
    {
        public int LockLevel { get; set; }

        public FLTV()
        {

        }

        public FLTV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            LockLevel = reader.ReadBytes<int>(base.Data);
        }
    }
}
