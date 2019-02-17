using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class KEYM : Record
    {
        public KEYM(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}