using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.PGRD
{
    public class PGRR : Subrecord
    {
        public short[,] NodeLinks { get; set; }

        public PGRR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            NodeLinks = new short[base.Data.Length/sizeof(int),2];

            for (int i = 0; i < NodeLinks.GetLength(0); i++)
            {
                NodeLinks[i,0] = reader.ReadBytes<short>(base.Data);
                NodeLinks[i,1] = reader.ReadBytes<short>(base.Data);
            }
            
        }
    }
}
