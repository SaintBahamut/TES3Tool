using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class ARMO : Record
    {
        public ARMO(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
