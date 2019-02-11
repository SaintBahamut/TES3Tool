using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class EFSH : Record
    {
        public EFSH(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}