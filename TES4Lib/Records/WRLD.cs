using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class WRLD : Record
    {
        public WRLD(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}