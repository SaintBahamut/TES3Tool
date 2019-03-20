using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class NAM0 : Subrecord
    {
        public NAM0(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
