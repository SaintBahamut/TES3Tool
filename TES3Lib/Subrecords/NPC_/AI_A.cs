using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class AI_A : Subrecord
    {
        public AI_A(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}