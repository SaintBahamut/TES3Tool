using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class SKIL : Record
    {
        public SKIL(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
