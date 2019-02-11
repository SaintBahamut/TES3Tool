using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class NPC_ : Record
    {
        public NPC_(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}