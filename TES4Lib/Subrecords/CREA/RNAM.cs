using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class RNAM : Subrecord
    {
        public RNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
