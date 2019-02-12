using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XCLL : Subrecord
    {
        /// <summary>
        /// eh need hex edit this shit
        /// </summary>
        public byte[] Lighting { get; set; }

        public XCLL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Lighting = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
