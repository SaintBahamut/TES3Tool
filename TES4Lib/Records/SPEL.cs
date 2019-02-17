using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class SPEL : Record
    {
        public SPEL(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}