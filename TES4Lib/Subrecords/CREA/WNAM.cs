using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class WNAM : Subrecord
    {
        public WNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
