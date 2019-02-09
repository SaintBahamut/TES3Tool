using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class REGN : Record
    {
        public REGN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
            IsImplemented = false;
        }
    }
}
