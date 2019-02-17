using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class TES4 : Record
    {
        public TES4(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}