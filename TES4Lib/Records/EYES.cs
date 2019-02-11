using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class EYES : Record
    {
        public EYES(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}