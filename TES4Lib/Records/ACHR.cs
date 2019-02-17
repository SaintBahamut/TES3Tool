using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class ACHR : Record
    {
        public ACHR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}