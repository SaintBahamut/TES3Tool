using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class AI_E : Subrecord
    {
        public AI_E(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}