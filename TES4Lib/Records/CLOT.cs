using TES4Lib.Structures.Base;

namespace TES4Lib.Records
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