using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class INFO : Record
    {
        public INFO(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}