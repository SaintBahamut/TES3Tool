using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class NIFZ : Subrecord
    {
        public NIFZ(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
