using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class LEVI : Record
    {
        public LEVI(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
