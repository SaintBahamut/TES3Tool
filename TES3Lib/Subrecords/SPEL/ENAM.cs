using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;
using Attribute = TES3Lib.Enums.Attribute;

namespace TES3Lib.Subrecords.SPEL
{
    /// <summary>
    /// Effect of spell
    /// </summary>
    public class ENAM : Subrecord
    {        
        public MagicEffect Effect { get; set; }
        public Skill Skill { get; set; }
        public Attribute Attribute { get; set; }
        public int Range { get; set; }
        public int Area { get; set; }
        public int Duration { get; set; }
        public int MagnitudeMin { get; set; }
        public int MagnitudeMax { get; set; }

        public ENAM()
        {
            Effect = MagicEffect.Blind;
            Skill = Skill.Unused;
            Attribute = Attribute.Unused;
        }

        public ENAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Effect = (MagicEffect)reader.ReadBytes<short>(base.Data);
            Skill = (Skill)reader.ReadBytes<byte>(base.Data);
            Attribute = (Attribute)reader.ReadBytes<byte>(base.Data);
            Range = reader.ReadBytes<int>(base.Data);
            Area = reader.ReadBytes<int>(base.Data);
            Duration = reader.ReadBytes<int>(base.Data);
            MagnitudeMin = reader.ReadBytes<int>(base.Data);
            MagnitudeMax = reader.ReadBytes<int>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();
       
            data.AddRange(ByteWriter.ToBytes((short)Effect, typeof(short)));
            data.Add((byte)Skill);
            data.Add((byte)Effect);
            data.AddRange(ByteWriter.ToBytes(Range, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Duration, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(MagnitudeMin, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(MagnitudeMax, typeof(int)));

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}
