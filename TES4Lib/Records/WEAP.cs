using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class WEAP : Record
    {
        public WEAP(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}