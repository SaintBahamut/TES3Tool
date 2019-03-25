using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class INDX : Subrecord
    {
        public int Unknown { get; set; }

        public INDX()
        {
        }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Unknown = reader.ReadBytes<int>(base.Data, base.Size);
        }
    }
}
