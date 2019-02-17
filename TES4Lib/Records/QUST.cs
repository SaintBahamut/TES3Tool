using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class QUST : Record
    {
        public QUST(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}