using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.SKILL
{
    public class INDX : Subrecord
    {
        /// <summary>
        /// Serialized to data as a 4-byte value.
        /// </summary>
        public Skill Skill { get; }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Skill = (Skill)reader.ReadBytes<int>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            data.AddRange(ByteWriter.ToBytes(Skill, typeof(int)));

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}