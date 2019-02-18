using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class CNDT : Subrecord
    {
        public CNDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}