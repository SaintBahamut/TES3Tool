using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class CSDT : Subrecord
    {
        public CSDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
