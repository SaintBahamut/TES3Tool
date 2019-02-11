using TES3Lib.Structures.Base;

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
