using TES3Lib.Base;

namespace TES3Lib.Records
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
