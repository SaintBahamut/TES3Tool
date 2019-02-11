using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class ACHR : Record
    {
        public ACHR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}