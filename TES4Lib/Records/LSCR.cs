using TES4Lib.Structures.Base;

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