using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class AI_T : Subrecord
    {
        public AI_T(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}