using TES4Lib.Structures.Base;

namespace TES4Lib.Records
{
    public class AMMO : Record
    {
        public AMMO(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}