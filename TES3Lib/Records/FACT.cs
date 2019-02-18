using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class FACT : Record
    {
        public FACT(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
