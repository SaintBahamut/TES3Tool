using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class LAND : Record
    {
        public LAND(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
