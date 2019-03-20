using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class NAM1 : Subrecord
    {
        public NAM1(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
