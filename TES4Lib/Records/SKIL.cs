using TES4Lib.Structures.Base;

namespace TES4Lib.Records
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