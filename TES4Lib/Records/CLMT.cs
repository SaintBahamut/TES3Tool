using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class CLMT : Record
    {
        public CLMT(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}