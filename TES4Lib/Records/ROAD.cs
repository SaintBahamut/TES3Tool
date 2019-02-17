using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class ROAD : Record
    {
        public ROAD(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}