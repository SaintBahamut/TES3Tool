using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.PGRD
{
    public class PGRR : Subrecord
    {
        public short[,] Edges { get; set; }

        public PGRR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Edges = new short[base.Data.Length/sizeof(int),2];

            for (int i = 0; i < Edges.GetLength(0); i++)
            {
                Edges[i,0] = reader.ReadBytes<short>(base.Data);
                Edges[i,1] = reader.ReadBytes<short>(base.Data);
            }
            
        }
    }
}
