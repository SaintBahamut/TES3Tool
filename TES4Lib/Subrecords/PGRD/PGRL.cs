using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.PGRD
{
    public class PGRL : Subrecord
    {
        public string LinkedObjectFormId { get; set; }

        public int[] LinkedNodes { get; set; }

        public PGRL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            LinkedObjectFormId = reader.ReadFormId(base.Data);

            int nodeCount = (base.Data.Length-4) / 4;

            LinkedNodes = new int[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                LinkedNodes[i] = reader.ReadBytes<int>(base.Data);             
            }
            
        }
    }
}
