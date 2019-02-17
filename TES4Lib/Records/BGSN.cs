using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class BGSN : Record
    {
        public BGSN(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}