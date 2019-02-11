using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class IDLE : Record
    {
        public IDLE(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}