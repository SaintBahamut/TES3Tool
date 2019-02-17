using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class GMST : Record
    {
        public GMST(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}