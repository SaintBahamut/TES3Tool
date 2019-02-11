using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class WRLD : Record
    {
        public WRLD(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}