using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class AIDT : Subrecord
    {
        public AIDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
