using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class ENCH : Record
    {
        public ENCH(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}