using TES4Lib.Base;

namespace TES4Lib.Records
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