using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class PROB : Record
    {
        public PROB(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
