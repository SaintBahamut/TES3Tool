using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class FLOR : Record
    {
        public FLOR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}