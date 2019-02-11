using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class REFR : Record
    {
        public REFR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}