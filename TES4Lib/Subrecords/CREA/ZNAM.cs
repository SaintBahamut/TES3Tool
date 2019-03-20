using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class ZNAM : Subrecord
    {
        public ZNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
