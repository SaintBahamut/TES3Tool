using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.CREA
{
    public class NPDT : Subrecord
    {
        public CreatureType Type { get; set; }

        public int Level { get; set; }

        public int Strength { get; set; }

        public int Willpower { get; set; }

        public int Agility { get; set; }

        public int Speed { get; set; }

        public int Endurance { get; set; }

        public int Personality { get; set; }

        public int Luck { get; set; }

        public int Health { get; set; }

        public int SpellPts { get; set; }

        public int Fatigue { get; set; }

        public int Soul { get; set; }

        public int Combat { get; set; }

        public int Magic { get; set; }

        public int Stealth { get; set; }

        public int AttackMin1 { get; set; }

        public int AttackMax1 { get; set; }

        public int AttackMin2 { get; set; }

        public int AttackMax2 { get; set; }

        public int AttackMin3 { get; set; }

        public int AttackMax3 { get; set; }

        public int Gold { get; set; }

        public NPDT()
        {

        }

        public NPDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = (CreatureType)reader.ReadBytes<int>(base.Data);
            Level = reader.ReadBytes<int>(base.Data);
            Strength = reader.ReadBytes<int>(base.Data);
            Willpower = reader.ReadBytes<int>(base.Data);
            Agility = reader.ReadBytes<int>(base.Data);
            Speed = reader.ReadBytes<int>(base.Data);
            Endurance = reader.ReadBytes<int>(base.Data);
            Personality = reader.ReadBytes<int>(base.Data);
            Luck = reader.ReadBytes<int>(base.Data);
            Health = reader.ReadBytes<int>(base.Data);
            SpellPts = reader.ReadBytes<int>(base.Data);
            Fatigue = reader.ReadBytes<int>(base.Data);
            Soul = reader.ReadBytes<int>(base.Data);
            Combat = reader.ReadBytes<int>(base.Data);
            Magic = reader.ReadBytes<int>(base.Data);
            Stealth = reader.ReadBytes<int>(base.Data);
            AttackMax1 = reader.ReadBytes<int>(base.Data);
            AttackMax1 = reader.ReadBytes<int>(base.Data);
            AttackMin2 = reader.ReadBytes<int>(base.Data);
            AttackMax2 = reader.ReadBytes<int>(base.Data);
            AttackMin3 = reader.ReadBytes<int>(base.Data);
            AttackMax3 = reader.ReadBytes<int>(base.Data);
            Gold = reader.ReadBytes<int>(base.Data);
        }
    }
}
