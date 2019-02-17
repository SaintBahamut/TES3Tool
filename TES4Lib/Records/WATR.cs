using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class WATR : Record
    {
        public WATR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}