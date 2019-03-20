using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class TNAM : Subrecord
    {
        public TNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
