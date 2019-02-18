using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class XSCL : Subrecord
    {
        public XSCL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}