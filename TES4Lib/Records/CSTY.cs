using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class CSTY : Record
    {
        public CSTY(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}