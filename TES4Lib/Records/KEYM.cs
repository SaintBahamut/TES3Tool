using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class KEYM : Record
    {
        public KEYM(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}