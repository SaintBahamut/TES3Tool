using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class EYES : Record
    {
        public EYES(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}