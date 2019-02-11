using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class LVLC : Record
    {
        public LVLC(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}