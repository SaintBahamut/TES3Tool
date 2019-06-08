using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums;
using TES4Lib.Enums.Flags;
using Utility;

namespace TES4Lib.Subrecords.SPEL
{
    /// <summary>
    /// Spell data
    /// </summary>
    public class SPIT : Subrecord
    {
        public SpellType Type { get; set; }

        public int SpellCost { get; set; }

        public SpellLevel SpellLevel { get; set; }

        public HashSet<SpellFlag> Flags { get; set; }

        public SPIT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = reader.ReadBytes<SpellType>(base.Data);
            SpellCost = reader.ReadBytes<int>(base.Data);
            SpellLevel = reader.ReadBytes<SpellLevel>(base.Data);
            Flags = reader.ReadFlagBytes<SpellFlag>(base.Data);
        }
    }
}
