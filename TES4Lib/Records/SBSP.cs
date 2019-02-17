using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class SBSP : Record
    {
        public SBSP(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}