using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class IDLE : Record
    {
        public IDLE(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}