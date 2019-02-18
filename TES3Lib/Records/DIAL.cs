using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class DIAL : Record
    {
        public DIAL(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
