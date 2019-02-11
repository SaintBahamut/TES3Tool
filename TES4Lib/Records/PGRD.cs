using TES4Lib.Structures.Base;

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