using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using TES3Lib.Enums.Flags;
using Utility;

namespace TES3Lib.Subrecords.BOOK
{
    public class BKDT : Subrecord
    {
        public float Weight { get; set; }

        public int Value { get; set; }

        /// <summary>
        /// Scroll	(1 is scroll, 0 is book)
        /// </summary>
        public BookFlag Flag { get; set; }

        /// <summary>
        /// SkillId of skill that book raises (-1 is no skill)
        /// </summary>
        public Skill Skill { get; set; }

        public int EnchantPoints { get; set; }

        public BKDT()
        {

        }

        public BKDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Flag = reader.ReadBytes<BookFlag>(base.Data);
            Skill = (Skill)reader.ReadBytes<int>(base.Data);
            EnchantPoints = reader.ReadBytes<int>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {
            var tes1 = (int)Flag;
            var tes2 = (int)Skill;

            List<byte> data = new List<byte>();
            data.AddRange(ByteWriter.ToBytes(Weight, typeof(float)));
            data.AddRange(ByteWriter.ToBytes(Value, typeof(int)));
            data.AddRange(ByteWriter.ToBytes((int)Flag, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Skill.Equals(Skill.Unused) ? UInt32.MaxValue : (uint)Skill, typeof(uint)));
            data.AddRange(ByteWriter.ToBytes(EnchantPoints, typeof(int)));

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}