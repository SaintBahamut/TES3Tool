using TES4Lib.Structures.Base;

namespace TES4Lib.Records
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