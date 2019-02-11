using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class CLAS: Record
    {
        public CLAS(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
