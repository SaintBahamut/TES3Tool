using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class REPA : Record
    {
        public REPA(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
