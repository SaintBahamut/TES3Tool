using TES4Lib.Structures.Base;

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