using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class CELL : Record
    {
        public CELL(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}