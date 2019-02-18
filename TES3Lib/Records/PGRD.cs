using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class PGRD : Record
    {
        public PGRD(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
