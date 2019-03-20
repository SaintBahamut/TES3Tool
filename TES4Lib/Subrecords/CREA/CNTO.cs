using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class CNTO : Subrecord
    {
        public CNTO(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
