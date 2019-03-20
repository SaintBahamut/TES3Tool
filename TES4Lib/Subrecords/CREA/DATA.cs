using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class DATA : Subrecord
    {
        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
