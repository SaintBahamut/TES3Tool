using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class ALCH : Record
    {
        public ALCH(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
