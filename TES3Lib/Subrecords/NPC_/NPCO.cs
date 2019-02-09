using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class NPCO : Subrecord
    {
        public NPCO(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
