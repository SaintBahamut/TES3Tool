using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class SOUN : Record
    {
        public SOUN(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
