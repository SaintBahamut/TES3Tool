using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class SLGM : Record
    {
        public SLGM(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}