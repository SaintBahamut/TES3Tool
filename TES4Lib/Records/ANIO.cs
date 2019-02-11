using TES4Lib.Structures.Base;

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