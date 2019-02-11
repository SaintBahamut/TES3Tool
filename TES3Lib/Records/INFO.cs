using TES3Lib.Structures.Base;

namespace TES3Lib.Records
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
