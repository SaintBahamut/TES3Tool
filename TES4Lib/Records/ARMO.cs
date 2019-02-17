using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class ARMO : Record
    {
        public ARMO(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}