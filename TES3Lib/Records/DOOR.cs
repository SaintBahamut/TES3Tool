using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class DOOR : Record
    {
        public DOOR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
