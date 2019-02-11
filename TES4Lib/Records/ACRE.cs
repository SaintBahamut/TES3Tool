using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class ACRE : Record
    {
        public ACRE(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}