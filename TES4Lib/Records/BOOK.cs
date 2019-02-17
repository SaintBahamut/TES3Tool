using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class BOOK : Record
    {
        public BOOK(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}