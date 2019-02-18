using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class REPA : Record
    {
        public REPA(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
