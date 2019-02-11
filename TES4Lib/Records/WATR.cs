using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class WATR : Record
    {
        public WATR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}