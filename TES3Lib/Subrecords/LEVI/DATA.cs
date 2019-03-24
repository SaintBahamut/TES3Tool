using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using Utility;

namespace TES3Lib.Subrecords.LEVI
{
    /// <summary>
    /// Leveled item flags
    /// </summary>
    public class DATA : Subrecord
    {
        public HashSet<LeveledItemFlag> Flag { get; set; }

        public DATA()
        {
        }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flag = reader.ReadFlagBytes<LeveledItemFlag>(base.Data);
        }
    }
}
