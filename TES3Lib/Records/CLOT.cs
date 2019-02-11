using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class CLOT : Record
    {
        public CLOT(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
