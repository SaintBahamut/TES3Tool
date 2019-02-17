using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class GLOB : Record
    {
        public GLOB(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}