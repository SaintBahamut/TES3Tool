using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class INGR : Record
    {
        public INGR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
