using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class LIGH : Record
    {
        public LIGH(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
