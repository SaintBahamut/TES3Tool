using TES3Lib.Structures.Base;

namespace TES3Lib.Records
{
    public class CONT : Record
    {
        public CONT(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
