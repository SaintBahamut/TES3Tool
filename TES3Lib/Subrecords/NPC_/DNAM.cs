using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class DNAM : Subrecord
    {
        public DNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}