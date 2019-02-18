using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class RACE: Record
    {
        public RACE(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
