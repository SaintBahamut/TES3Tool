using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class PROB : Record
    {
        public PROB(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
            IsImplemented = false;
        }
    }
}
