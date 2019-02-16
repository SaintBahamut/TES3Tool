using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XCLL : Subrecord
    {
        /// <summary>
        /// eh need hex edit this shit
        /// </summary>
        /// 
        public int Ambient { get; set; }
        public int Directional { get; set; }
        public int Fog { get; set; }
       
        public byte[] Lighting { get; set; }

        public XCLL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Ambient = reader.ReadBytes<int>(base.Data);
            Directional = reader.ReadBytes<int>(base.Data);
            Fog = reader.ReadBytes<int>(base.Data);
        }
    }
}
