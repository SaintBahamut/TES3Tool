using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature stats
    /// </summary>
    public class DATA : Subrecord
    {
        public CreatureType CreatureType { get; set; }

        public byte CombatSkill { get; set; }

        public byte MagicSkill { get; set; }

        public byte StealthSkill { get; set; }

        public SoulGemType Soul { get; set; }

        public ushort Health { get; set; }

        public ushort Unused { get; set; }

        public ushort AttackDamage { get; set; }

        public byte Strength { get; set; }

        public byte Intelligence { get; set; }

        public byte Willpower { get; set; }

        public byte Agility { get; set; }

        public byte Speed { get; set; }

        public byte Endurance { get; set; }

        public byte Personality { get; set; }

        public byte Luck { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            CreatureType = reader.ReadBytes<CreatureType>(base.Data);
            CombatSkill = reader.ReadBytes<byte>(base.Data);
            MagicSkill = reader.ReadBytes<byte>(base.Data);
            StealthSkill = reader.ReadBytes<byte>(base.Data);
            Soul = reader.ReadBytes<SoulGemType>(base.Data);
            Health = reader.ReadBytes<ushort>(base.Data);
            Unused = reader.ReadBytes<ushort>(base.Data);
            AttackDamage = reader.ReadBytes<ushort>(base.Data);
            Strength = reader.ReadBytes<byte>(base.Data);
            Intelligence = reader.ReadBytes<byte>(base.Data);
            Willpower = reader.ReadBytes<byte>(base.Data);
            Agility = reader.ReadBytes<byte>(base.Data);
            Speed = reader.ReadBytes<byte>(base.Data);
            Endurance = reader.ReadBytes<byte>(base.Data);
            Personality = reader.ReadBytes<byte>(base.Data);
            Luck = reader.ReadBytes<byte>(base.Data);
        }
    }
}
