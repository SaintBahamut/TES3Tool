using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class AI_F : Subrecord
    {
        public AI_F(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}