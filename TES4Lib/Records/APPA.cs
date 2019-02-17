using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class APPA : Record
    {
        public APPA(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}