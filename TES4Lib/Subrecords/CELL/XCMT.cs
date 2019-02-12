using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XCMT : Subrecord
    {
        /// <summary>
        /// Music type if not default type
        /// 0x01 = Public
        /// 0x02 = Dungeon
        /// </summary>
        public byte MusicType { get; set; }

        public XCMT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MusicType = reader.ReadBytes<byte[]>(base.Data, base.Size)[0];
        }
    }
}
