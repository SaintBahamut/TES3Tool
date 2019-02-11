using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class FURN : Record
    {
        public FURN(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}