using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class LTEX : Record
    {
        public LTEX(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
