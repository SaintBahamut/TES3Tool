using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class PACK : Record
    {
        public PACK(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}