using TES4Lib.Base;

namespace TES4Lib.Records
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