using TES4Lib.Base;

namespace TES4Lib.Records
{
    public class TREE : Record
    {
        public TREE(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}