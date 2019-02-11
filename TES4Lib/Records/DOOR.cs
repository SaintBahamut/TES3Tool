using TES4Lib.Structures.Base;

namespace TES4Lib.Records
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