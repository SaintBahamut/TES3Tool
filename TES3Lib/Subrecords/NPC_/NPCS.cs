using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class NPCS : Subrecord
    {
        public NPCS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
