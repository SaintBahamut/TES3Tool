using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class MGEF : Record
    {
        public MGEF(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
