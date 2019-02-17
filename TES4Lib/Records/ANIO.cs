using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class ANIO : Record
    {
        public ANIO(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}