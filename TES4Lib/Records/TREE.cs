using TES4Lib.Structures.Base;

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