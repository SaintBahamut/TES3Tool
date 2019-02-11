using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class SGST : Record
    {
        public SGST(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}