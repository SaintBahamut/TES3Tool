using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class SPEL : Record
    {
        public SPEL(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
