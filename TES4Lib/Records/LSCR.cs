using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class LSCR : Record
    {
        public LSCR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}