using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class ACBS : Subrecord
    {
        public ACBS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
