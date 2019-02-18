using TES3Lib.Base;

namespace TES3Lib.Records
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
