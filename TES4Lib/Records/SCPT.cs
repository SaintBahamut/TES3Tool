using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class SCPT : Record
    {
        public SCPT(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}