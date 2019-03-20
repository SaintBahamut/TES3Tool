using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class CSDI : Subrecord
    {
        public CSDI(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
