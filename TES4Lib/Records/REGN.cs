using TES4Lib.Structures.Base;

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