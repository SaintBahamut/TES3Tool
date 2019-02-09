using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class BSGN : Record
    {
        public BSGN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
