using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.RACE
{
    public class RADT : Subrecord
    {
        public SkillBonus[] SkillBonuses { get; set; }

        public int[] Strength { get; set; }

        public int[] Intelligence { get; set; }

        public int[] Willpower { get; set; }

        public int[] Agility { get; set; }

        public int[] Speed { get; set; }

        public int[] Endurance { get; set; }

        public int[] Personality { get; set; }

        public int[] Luck { get; set; }

        public float[] Height { get; set; }

        public float[] Weight { get; set; }

        /// <summary>
        /// 1 = Playable
		//	2 = Beast Race
        /// </summary>
        public int Flags { get; set; }

        public RADT()
        {
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

            Strength = new int[2];
            Intelligence = new int[2];
            Strength = new int[2];
            Willpower = new int[2];
            Agility = new int[2];
            Speed = new int[2];
            Endurance = new int[2];
            Personality = new int[2];
            Luck = new int[2];
            Height = new float[2];
            Weight = new float[2];

            Strength[0] = reader.ReadBytes<int>(base.Data);
            Strength[1] = reader.ReadBytes<int>(base.Data);

            Intelligence[0] = reader.ReadBytes<int>(base.Data);
            Intelligence[1] = reader.ReadBytes<int>(base.Data);

            Willpower[0] = reader.ReadBytes<int>(base.Data);
            Willpower[1] = reader.ReadBytes<int>(base.Data);

            Agility[0] = reader.ReadBytes<int>(base.Data);
            Agility[1] = reader.ReadBytes<int>(base.Data);

            Speed[0] = reader.ReadBytes<int>(base.Data);
            Speed[1] = reader.ReadBytes<int>(base.Data);

            Endurance[0] = reader.ReadBytes<int>(base.Data);
            Endurance[1] = reader.ReadBytes<int>(base.Data);

            Personality[0] = reader.ReadBytes<int>(base.Data);
            Personality[1] = reader.ReadBytes<int>(base.Data);

            Luck[0] = reader.ReadBytes<int>(base.Data);
            Luck[1] = reader.ReadBytes<int>(base.Data);

            Height[0] = reader.ReadBytes<float>(base.Data);
            Height[1] = reader.ReadBytes<float>(base.Data);

            Weight[0] = reader.ReadBytes<float>(base.Data);
            Weight[1] = reader.ReadBytes<float>(base.Data);

            Flags = reader.ReadBytes<int>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            for (int i = 0; i < 7; i++)
            {
                data.AddRange(ByteWriter.ToBytes(SkillBonuses[i].Skill, typeof(Skill)));
                data.AddRange(ByteWriter.ToBytes(SkillBonuses[i].Bonus, typeof(int)));
            }
            data.AddRange(ByteWriter.ToBytes(Strength[0], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Strength[1], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Intelligence[0], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Intelligence[1], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Willpower[0], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Willpower[1], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Agility[0], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Agility[1], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Speed[0], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Speed[1], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Endurance[0], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Endurance[1], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Personality[0], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Personality[1], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Luck[0], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Luck[1], typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Height[0], typeof(float)));
            data.AddRange(ByteWriter.ToBytes(Height[1], typeof(float)));
            data.AddRange(ByteWriter.ToBytes(Weight[0], typeof(float)));
            data.AddRange(ByteWriter.ToBytes(Weight[1], typeof(float)));
            data.AddRange(ByteWriter.ToBytes(Flags, typeof(int)));

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }

        public struct SkillBonus
        {
            public Skill Skill;
            public int Bonus;
        }
    }
}
