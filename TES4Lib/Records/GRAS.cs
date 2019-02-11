using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class GRAS : Record
    {
        public GRAS(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}