using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.PGRD
{
    public class PGRI : Subrecord
    {
        public GridNodeExternal[] GridNodesExternal { get; set; }

        public PGRI(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            int nodeCount = base.Data.Length / 16;
            GridNodesExternal = new GridNodeExternal[nodeCount];

            for (int i = 0; i < nodeCount; i++)
            {
                GridNodesExternal[i].LocalNodeNumber = reader.ReadBytes<int>(base.Data);
                GridNodesExternal[i].ExternalNodeX = reader.ReadBytes<float>(base.Data);
                GridNodesExternal[i].ExternalNodeY = reader.ReadBytes<float>(base.Data);
                GridNodesExternal[i].ExternalNodeZ = reader.ReadBytes<float>(base.Data);
            }
            
        }

        public struct GridNodeExternal
        {
            public int LocalNodeNumber;
            public float ExternalNodeX;
            public float ExternalNodeY;
            public float ExternalNodeZ;
        }
    }
}
