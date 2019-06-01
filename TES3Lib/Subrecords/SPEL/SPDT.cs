using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Enums;
using TES3Lib.Enums.Flags;
using Utility;

namespace TES3Lib.Subrecords.SPEL
{
    /// <summary>
    /// Spell data
    /// </summary>
    public class SPDT : Subrecord
    {
        public Spell Type { get; set; }

        public int SpellCost { get; set; }

        public HashSet<SpellFlag> Flags { get; set; }

        public SPDT()
        {

        }

        public SPDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = reader.ReadBytes<Spell>(base.Data);
            SpellCost = reader.ReadBytes<int>(base.Data);
            Flags = reader.ReadFlagBytes<SpellFlag>(base.Data);
        }
    }
}
