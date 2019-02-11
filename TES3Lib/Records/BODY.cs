using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class BODY : Record
    {
        public BODY(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
