using TES4Lib.Base;
using Utility;
using TES4Lib.Enums.Flags;
using System.Collections.Generic;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Level list flags (1 byte)
    /// </summary>
    public class LVLF : Subrecord
    {
        /// <summary>
        /// This 1 byte (confirmed) subrecord stores the leveled list flags.
        /// 0x01 = Calculate for All Levels <= Player's Level
        /// 0x02 = Calculate for Each Item in Count
        /// Some of the older leveled list entries do not have an LVLF subrecord.Instead, the high-order bit of the LVLD subrecord is used for the "calculate for all levels 
        /// <= player's level" flag, and the DATA subrecord is used for the "calculate for each item in count" flag.
        /// </summary>
        public HashSet<LeveledItemFlag> Flags { get; set; }

        public LVLF(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadFlagBytes<LeveledItemFlag>(base.Data);
        }
    }
}
