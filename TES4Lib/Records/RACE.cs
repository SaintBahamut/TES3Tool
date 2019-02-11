using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class RACE : Record
    {
        public RACE(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}