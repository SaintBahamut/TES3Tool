using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums.Flags;
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
        public HashSet<CellFlag> Flags { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadBytes<CellFlag>(base.Data);
        }
    }
}
