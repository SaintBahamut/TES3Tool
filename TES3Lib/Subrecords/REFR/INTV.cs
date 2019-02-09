using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class INTV : Subrecord
    {
        public int NumberOfUses { get; set; }

        public INTV()
        {
            NumberOfUses = 1;
        }

        public INTV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            NumberOfUses = reader.ReadBytes<int>(base.Data);
        }

        
    }
}