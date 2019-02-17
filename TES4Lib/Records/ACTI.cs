using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class ACTI : Record
    {
        public ACTI(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}