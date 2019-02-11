using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class CSTY : Record
    {
        public CSTY(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}