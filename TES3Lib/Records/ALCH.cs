using TES3Lib.Structures.Base;

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
