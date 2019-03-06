using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;
using Attribute = TES3Lib.Enums.Attribute;

namespace TES3Lib.Subrecords.FACT
{
    public class FADT : Subrecord
    {
        public Attribute FirstAttribute { get; set; }

        public Attribute SecondAttributre { get; set; }

        public RankRequirement[] RankData { get; set; }

        public Skill[] FavoredSkills { get; set; }

        public int Unknown { get; set; }

        /// <summary>
        /// 0 or 1
        /// </summary>
        public int IsHiddenFromPlayer { get; set; }

        public FADT()
        {
        }

        public FADT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            RankData = new RankRequirement[10];
            for (int i = 0; i < RankData.Length; i++)
            {
                RankData[i].FirstAttribute = reader.ReadBytes<int>(base.Data);
                RankData[i].SecondAttribute = reader.ReadBytes<int>(base.Data);
                RankData[i].FirstSkill = reader.ReadBytes<int>(base.Data);
                RankData[i].SecondSkill = reader.ReadBytes<int>(base.Data);
                RankData[i].Reputation = reader.ReadBytes<int>(base.Data);
            }

            FavoredSkills = new Skill[6];
            for (int i = 0; i < FavoredSkills.Length; i++)
            {
                FavoredSkills[i] = (Skill)reader.ReadBytes<int>(base.Data);
            }

            Unknown = reader.ReadBytes<int>(base.Data);
            IsHiddenFromPlayer = reader.ReadBytes<int>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {
            var properties = this.GetType()
                .GetProperties(System.Reflection.BindingFlags.Public |
                               System.Reflection.BindingFlags.Instance |
                               System.Reflection.BindingFlags.DeclaredOnly)
                               .OrderBy(x => x.MetadataToken)
                               .ToList();

            List<byte> data = new List<byte>();
         
            data.AddRange(ByteWriter.ToBytes(FirstAttribute, typeof(Attribute)));
            data.AddRange(ByteWriter.ToBytes(SecondAttributre, typeof(Attribute)));

            for (int i = 0; i < RankData.Length; i++)
            {
                data.AddRange(ByteWriter.ToBytes(RankData[i].FirstAttribute, typeof(int)));
                data.AddRange(ByteWriter.ToBytes(RankData[i].SecondAttribute, typeof(int)));
                data.AddRange(ByteWriter.ToBytes(RankData[i].FirstSkill, typeof(int)));
                data.AddRange(ByteWriter.ToBytes(RankData[i].SecondSkill, typeof(int)));
                data.AddRange(ByteWriter.ToBytes(RankData[i].Reputation, typeof(int)));
            }

            for (int i = 0; i < FavoredSkills.Length; i++)
            {
                data.AddRange(ByteWriter.ToBytes(FavoredSkills[i], typeof(Skill)));
            }

            data.AddRange(ByteWriter.ToBytes(Unknown, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(IsHiddenFromPlayer, typeof(int)));

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }

        /// <summary>
        /// Requirements for faction rank
        /// </summary>
        public struct RankRequirement
        {
            public int FirstAttribute;
            public int SecondAttribute;
            public int FirstSkill;
            public int SecondSkill;
            public int Reputation;
        }
    }
}
