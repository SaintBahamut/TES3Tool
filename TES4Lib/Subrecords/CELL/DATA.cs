using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class DATA : Subrecord
    {
        /// <summary>
        /// 0x01 = Can't travel from here
        /// 0x02 = Has water
        /// 0x08 = Force hide land(exterior cell), Oblivion interior(interior cell)
        /// 0x20 = Public place
        /// 0x40 = Hand changed
        /// 0x80 = Behave like exterior
        /// </summary>
        public byte Flag { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flag = reader.ReadBytes<byte[]>(base.Data, base.Size)[0];
        }
    }
}
