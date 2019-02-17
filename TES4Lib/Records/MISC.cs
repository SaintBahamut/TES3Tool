using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class MISC : Record
    {
        public MISC(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}