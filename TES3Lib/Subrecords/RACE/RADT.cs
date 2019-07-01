using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using TES3Lib.Enums.Flags;
using Utility;

namespace TES3Lib.Subrecords.RACE
{
    /// <summary>
    /// Base attributes and skill bonuses
    /// </summary>
    public class RADT : Subrecord
    {
        public SkillBonus[] SkillBonuses { get; set; }

        public Attributes Male { get; set; }

        public Attributes Female { get; set; }

        public HashSet<RaceFlags> Flags { get; set; }

        public RADT()
        {
            SkillBonuses = new SkillBonus[7];

            for (int i = 0; i < 7; i++)
                SkillBonuses[i] = new SkillBonus();

            Male = new Attributes();
            Female = new Attributes();
            Flags = new HashSet<RaceFlags>();
        }

        public RADT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            
            SkillBonuses = new SkillBonus[7];
            for (int i = 0; i < 7; i++)
            {
                SkillBonuses[i].Skill = (Skill)reader.ReadBytes<int>(base.Data);
                SkillBonuses[i].Bonus = reader.ReadBytes<int>(base.Data);
            }

            Male = new Attributes();
            Female = new Attributes();

            Male.Strength = reader.ReadBytes<int>(base.Data);
            Female.Strength = reader.ReadBytes<int>(base.Data);

            Male.Intelligence = reader.ReadBytes<int>(base.Data);
            Female.Intelligence = reader.ReadBytes<int>(base.Data);

            Male.Willpower = reader.ReadBytes<int>(base.Data);
            Female.Willpower = reader.ReadBytes<int>(base.Data);

            Male.Agility = reader.ReadBytes<int>(base.Data);
            Female.Agility = reader.ReadBytes<int>(base.Data);

            Male.Speed = reader.ReadBytes<int>(base.Data);
            Female.Speed = reader.ReadBytes<int>(base.Data);

            Male.Endurance = reader.ReadBytes<int>(base.Data);
            Female.Endurance = reader.ReadBytes<int>(base.Data);

            Male.Personality = reader.ReadBytes<int>(base.Data);
            Female.Personality = reader.ReadBytes<int>(base.Data);

            Male.Luck = reader.ReadBytes<int>(base.Data);
            Female.Luck = reader.ReadBytes<int>(base.Data);

            Male.Height = reader.ReadBytes<float>(base.Data);
            Female.Height = reader.ReadBytes<float>(base.Data);

            Male.Weight = reader.ReadBytes<float>(base.Data);
            Female.Weight = reader.ReadBytes<float>(base.Data);

            Flags = reader.ReadFlagBytes<RaceFlags>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            for (int i = 0; i < 7; i++)
            {
                data.AddRange(SkillBonuses[i].Skill.Equals(Skill.Unused) ? new byte[] { 255, 255, 255, 255 } : ByteWriter.ToBytes((int)SkillBonuses[i].Skill, typeof(int)));
                data.AddRange(ByteWriter.ToBytes((int)SkillBonuses[i].Bonus, typeof(int)));
            }
            data.AddRange(ByteWriter.ToBytes(Male.Strength, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Female.Strength, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Male.Intelligence, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Female.Intelligence, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Male.Willpower, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Female.Willpower, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Male.Agility, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Female.Agility, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Male.Speed, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Female.Speed, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Male.Endurance, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Female.Endurance, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Male.Personality, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Female.Personality, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Male.Luck, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Female.Luck, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Male.Height, typeof(float)));
            data.AddRange(ByteWriter.ToBytes(Female.Height, typeof(float)));
            data.AddRange(ByteWriter.ToBytes(Male.Weight, typeof(float)));
            data.AddRange(ByteWriter.ToBytes(Female.Weight, typeof(float)));
            data.AddRange(ByteWriter.ToBytes(SerializeFlag(Flags), typeof(int)));

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }

        public class SkillBonus
        {
            public Skill Skill;
            public int Bonus;

            public SkillBonus()
            {
                Skill = Skill.Unused;
                Bonus = 0;
            }
        }

        public class Attributes
        {
            public int Strength { get; set; }
            public int Intelligence { get; set; }
            public int Willpower { get; set; }
            public int Agility { get; set; }
            public int Speed { get; set; }
            public int Endurance { get; set; }
            public int Personality { get; set; }
            public int Luck { get; set; }
            public float Weight { get; set; }
            public float Height { get; set; }

            public Attributes()
            {
                Strength = 50;
                Intelligence = 50;
                Willpower = 50;
                Agility = 50;
                Speed = 50;
                Endurance = 50;
                Personality = 50;
                Luck = 50;
                Weight = 1;
                Height = 1;
            }
        }
    }
}
