using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class RACE: Record
    {
        public RACE(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
            IsImplemented = false;
        }
    }
}
