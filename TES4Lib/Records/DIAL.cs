using TES4Lib.Base;

namespace TES4Lib.Records
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