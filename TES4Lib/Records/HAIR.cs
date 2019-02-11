using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class HAIR : Record
    {
        public HAIR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}