using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;
using Utility.Attributes;
using static Utility.Common;
using Attribute = TES3Lib.Enums.Attribute;

namespace TES3Lib.Subrecords.FACT
{
    public class FADT : Subrecord
    {
        public Attribute FirstAttribute { get; set; }

        public Attribute SecondAttributre { get; set; }

        public RankRequirement[] RankData { get; set; }

        public Skill[] FavoredSkills { get; set; }

        public uint Unknown { get; set; }

        [SizeInBytes(4)]
        public bool IsHiddenFromPlayer { get; set; }

        public FADT()
        {
            Unknown = 0xFFFFFFFF;
        }

        public FADT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            FirstAttribute = reader.ReadBytes<Attribute>(base.Data);
            SecondAttributre = reader.ReadBytes<Attribute>(base.Data);

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
                FavoredSkills[i] = reader.ReadBytes<Skill>(rawData);
            }

            Unknown = reader.ReadBytes<int>(base.Data);
            IsHiddenFromPlayer = reader.ReadBytes<int>(base.Data) == 0 ? false : true;
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
         
            data.AddRange(ByteWriter.ToBytes(FirstAttribute, typeof(uint)));
            data.AddRange(ByteWriter.ToBytes(SecondAttributre, typeof(uint)));

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
                data.AddRange(ByteWriter.ToBytes(FavoredSkills[i], typeof(uint)));
            }

            data.AddRange(ByteWriter.ToBytes(Unknown, typeof(int)));

           var getSizeProp = GetAttributeFromType<SizeInBytesAttribute>(this.GetType().GetProperty("IsHiddenFromPlayer"));
           data.AddRange(ByteWriter.ToBytes(IsHiddenFromPlayer, typeof(bool), getSizeProp));

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
