using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class QUST : Record
    {
        public QUST(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}