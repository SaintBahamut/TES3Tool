using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class CSCR : Subrecord
    {
        public CSCR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
