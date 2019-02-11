using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class LTEX : Record
    {
        public LTEX(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
