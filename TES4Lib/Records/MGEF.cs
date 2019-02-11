using TES4Lib.Structures.Base;

namespace TES4Lib.Records
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