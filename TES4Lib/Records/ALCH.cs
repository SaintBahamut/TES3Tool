using TES4Lib.Structures.Base;

namespace TES4Lib.Records
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