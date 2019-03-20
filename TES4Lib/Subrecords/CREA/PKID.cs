using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class PKID : Subrecord
    {
        public PKID(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
