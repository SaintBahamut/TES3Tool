using TES3Lib.Structures.Base;

namespace TES3Lib.Records
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
