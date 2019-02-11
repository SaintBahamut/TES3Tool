using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class LAND : Record
    {
        public LAND(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}