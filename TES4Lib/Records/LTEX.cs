using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class LTEX : Record
    {
        public LTEX(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}