using TES3Lib.Structures.Base;

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
