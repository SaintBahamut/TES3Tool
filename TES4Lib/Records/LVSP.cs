using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class LVSP : Record
    {
        public LVSP(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}