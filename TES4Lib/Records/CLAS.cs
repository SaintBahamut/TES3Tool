using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class CLAS : Record
    {
        public CLAS(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}