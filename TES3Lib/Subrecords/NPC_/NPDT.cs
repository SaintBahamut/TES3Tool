using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class NPDT : Subrecord
    {
        public NPDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
