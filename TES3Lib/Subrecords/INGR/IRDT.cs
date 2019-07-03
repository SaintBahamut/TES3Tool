using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;
using Utility.Attributes;
using Attribute = TES3Lib.Enums.Attribute;

namespace TES3Lib.Subrecords.INGR
{
    public class IRDT : Subrecord
    {
        public float Weight { get; set; }

        public int Value { get; set; }

        /// <summary>
        /// Most relevant, decides if SkillId or AttributeId is used
        /// </summary>
        [SizeInBytes(4)]
        public MagicEffect[] EffectIds { get; set; }

        /// <summary>
        /// Default is skill with Id 0 (Block), only used with appriorpate EffectId
        /// </summary>
        public Skill[] SkillIds { get; set; }

        /// <summary>
        /// Default is attribute with Id 0 (Strength), only used with appriorpate EffectId
        /// </summary>
        public Attribute[] AttributeIds { get; set; }

        public IRDT()
        {
        }

        public IRDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            EffectIds = new MagicEffect[]
            {
                reader.ReadBytes<MagicEffect>(base.Data),
                reader.ReadBytes<MagicEffect>(base.Data),
                reader.ReadBytes<MagicEffect>(base.Data),
                reader.ReadBytes<MagicEffect>(base.Data),
            };
            SkillIds = new Skill[]
            {
                reader.ReadBytes<Skill>(base.Data),
                reader.ReadBytes<Skill>(base.Data),
                reader.ReadBytes<Skill>(base.Data),
                reader.ReadBytes<Skill>(base.Data),
            };
            AttributeIds = new Attribute[]
            {
                reader.ReadBytes<Attribute>(base.Data),
                reader.ReadBytes<Attribute>(base.Data),
                reader.ReadBytes<Attribute>(base.Data),
                reader.ReadBytes<Attribute>(base.Data),
            };
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            data.AddRange(ByteWriter.ToBytes(Weight, typeof(float)));
            data.AddRange(ByteWriter.ToBytes(Value, typeof(int)));

            for (int i = 0; i < EffectIds.Length; i++)
            {
                data.AddRange(ByteWriter.ToBytes(Convert.ToInt32(EffectIds[i]), typeof(int)));
            }

            for (int i = 0; i < SkillIds.Length; i++)
            {
                data.AddRange(ByteWriter.ToBytes(SkillIds[i], typeof(uint)));
            }

            for (int i = 0; i < AttributeIds.Length; i++)
            {
                data.AddRange(ByteWriter.ToBytes(AttributeIds[i], typeof(uint)));
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}
