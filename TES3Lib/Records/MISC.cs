using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class MISC : Record
    {
        public MISC(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
