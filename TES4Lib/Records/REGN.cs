using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class REGN : Record
    {
        public REGN(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}