using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class CSDC : Subrecord
    {
        public CSDC(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
