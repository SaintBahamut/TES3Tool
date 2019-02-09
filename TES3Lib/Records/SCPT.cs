using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class SCPT: Record
    {
        public SCPT(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
            IsImplemented = false;
        }
    }
}
