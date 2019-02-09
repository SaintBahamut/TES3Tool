using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class SOUN : Record
    {
        public SOUN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
            IsImplemented = false;
        }
    }
}
