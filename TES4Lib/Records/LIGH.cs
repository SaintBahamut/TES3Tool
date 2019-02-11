using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class LICH : Record
    {
        public LICH(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}