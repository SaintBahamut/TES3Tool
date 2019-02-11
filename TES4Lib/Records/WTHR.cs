using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class WTHR : Record
    {
        public WTHR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}