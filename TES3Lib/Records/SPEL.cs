using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class SPEL : Record
    {
        public SPEL(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
            IsImplemented = false;
        }
    }
}
