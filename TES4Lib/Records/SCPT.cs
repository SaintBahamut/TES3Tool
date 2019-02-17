using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class SCPT : Record
    {
        public SCPT(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}