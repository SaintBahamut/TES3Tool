using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class ENCH : Record
    {
        public ENCH(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}