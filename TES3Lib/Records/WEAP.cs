using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class WEAP : Record
    {
        public WEAP(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
