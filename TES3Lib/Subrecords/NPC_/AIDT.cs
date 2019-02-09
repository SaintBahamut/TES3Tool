using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class AIDT : Subrecord
    {
        public AIDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}