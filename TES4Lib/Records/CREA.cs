using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class CREA : Record
    {
        public CREA(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}