using TES4Lib.Structures.Base;

namespace TES4Lib.Records
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