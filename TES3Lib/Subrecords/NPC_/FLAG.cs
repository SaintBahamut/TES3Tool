using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class FLAG : Subrecord
    {
        public FLAG(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
