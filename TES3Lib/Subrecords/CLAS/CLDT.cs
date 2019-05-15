using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using TES3Lib.Enums.Flags;
using Utility;
using Attribute = TES3Lib.Enums.Attribute;

namespace TES3Lib.Subrecords.CLAS
{
    public class CLDT : Subrecord
    {
        public Attribute PrimaryAttribute1 { get; set; }

        public Attribute PrimaryAttribute2 { get; set; }

        public Specialization Specialization { get; set; }

        public Skill Minor1 { get; set; }

        public Skill Major1 { get; set; }

        public Skill Minor2 { get; set; }

        public Skill Major2 { get; set; }

        public Skill Minor3 { get; set; }

        public Skill Major3 { get; set; }

        public Skill Minor4 { get; set; }

        public Skill Major4 { get; set; }

        public Skill Minor5 { get; set; }

        public Skill Major5 { get; set; }

        /// <summary>
        /// Flags
		///	0x0001 = Playable
        /// </summary>
        public int IsPlayable { get; set; }

        public HashSet<ServicesFlag> Services { get; set; }

        public CLDT()
        {
        }

        public CLDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            PrimaryAttribute1 = (Attribute)reader.ReadBytes<int>(base.Data);
            PrimaryAttribute2 = (Attribute)reader.ReadBytes<int>(base.Data);
            Specialization = (Specialization)reader.ReadBytes<int>(base.Data);
            Minor1 = (Skill)reader.ReadBytes<int>(base.Data);
            Major1 = (Skill)reader.ReadBytes<int>(base.Data);
            Minor2 = (Skill)reader.ReadBytes<int>(base.Data);
            Major2 = (Skill)reader.ReadBytes<int>(base.Data);
            Minor3 = (Skill)reader.ReadBytes<int>(base.Data);
            Major3 = (Skill)reader.ReadBytes<int>(base.Data);
            Minor4 = (Skill)reader.ReadBytes<int>(base.Data);
            Major4 = (Skill)reader.ReadBytes<int>(base.Data);
            Minor5 = (Skill)reader.ReadBytes<int>(base.Data);
            Major5 = (Skill)reader.ReadBytes<int>(base.Data);
            IsPlayable = reader.ReadBytes<int>(base.Data);
            Services = reader.ReadFlagBytes<ServicesFlag>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            data.AddRange(ByteWriter.ToBytes(PrimaryAttribute1, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(PrimaryAttribute2, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Specialization, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Minor1, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Major1, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Minor2, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Major2, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Minor3, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Major3, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Minor4, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Major4, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Minor5, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(Major5, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(IsPlayable, typeof(int)));
            data.AddRange(ByteWriter.ToBytes(SerializeFlag(Services), typeof(int)));

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}
