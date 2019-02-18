using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class SCPT: Record
    {
        public SCPT(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
