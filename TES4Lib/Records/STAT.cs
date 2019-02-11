using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class STAT : Record
    {
        public STAT(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}