using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums.Flags;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature config
    /// </summary>
    public class ACBS : Subrecord
    {
        public HashSet<CreatureFlag> Flags { get; set; }

        public ushort BaseSpellPoints { get; set; }

        public ushort Fatigue { get; set; }

        public ushort BarterGold { get; set; }

        public short LevelOffset { get; set; }

        public ushort CalcMin { get; set; }

        public ushort CalcMax { get; set; }

        public ACBS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadFlagBytes<CreatureFlag>(base.Data);
            BaseSpellPoints = reader.ReadBytes<ushort>(base.Data);
            Fatigue = reader.ReadBytes<ushort>(base.Data);
            BarterGold = reader.ReadBytes<ushort>(base.Data);
            LevelOffset = reader.ReadBytes<short>(base.Data);
            CalcMin = reader.ReadBytes<ushort>(base.Data);
            CalcMax = reader.ReadBytes<ushort>(base.Data);
        }
    }
}
