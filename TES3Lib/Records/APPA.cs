using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class APPA : Record
    {
        public APPA(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
