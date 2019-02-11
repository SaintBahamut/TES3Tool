using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class SKIL : Record
    {
        public SKIL(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
