using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class LEVC : Record
    {
        public LEVC(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
