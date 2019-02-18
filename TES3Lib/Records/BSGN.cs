using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class BSGN : Record
    {
        public BSGN(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
