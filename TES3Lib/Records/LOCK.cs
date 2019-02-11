using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class LOCK : Record
    {
        public LOCK(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
