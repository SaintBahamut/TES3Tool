using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class SNDG : Record
    {
        public SNDG(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
