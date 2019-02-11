using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class LVLI : Record
    {
        public LVLI(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}