using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class AI_W : Subrecord
    {
        public AI_W(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}