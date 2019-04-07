using TES4Lib.Base;
using Utility;
using TES4Lib.Enums.Flags;
using System.Collections.Generic;

namespace TES4Lib.Subrecords.NPC_
{
    public class ACBS : Subrecord
    {
        public HashSet<NPCFlag> Flags { get; set; }

        public ushort BaseSpellPoints { get; set; }

        public ushort Fatigue { get; set; }

        public ushort Gold { get; set; }

        public short Level { get; set; }

        public ushort CalcMin { get; set; }

        public ushort CalcMax { get; set; }

        public ACBS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadFlagBytes<NPCFlag>(this.Data);
            BaseSpellPoints = reader.ReadBytes<ushort>(this.Data);
            Fatigue = reader.ReadBytes<ushort>(this.Data);
            Gold = reader.ReadBytes<ushort>(this.Data);
            Level = reader.ReadBytes<short>(this.Data);
            CalcMin = reader.ReadBytes<ushort>(this.Data);
            CalcMax = reader.ReadBytes<ushort>(this.Data);
        }
    }
}
