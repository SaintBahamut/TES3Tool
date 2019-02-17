using TES4Lib.Base;

namespace TES4Lib.Records
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