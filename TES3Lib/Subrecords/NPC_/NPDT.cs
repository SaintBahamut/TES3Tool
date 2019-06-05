using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class NPDT : Subrecord
    {
        public short Level { get; set; }

        public byte Strength { get; set; }

        public byte Intelligence { get; set; }

        public byte Willpower { get; set; }

        public byte Agility { get; set; }

        public byte Speed { get; set; }

        public byte Endurance { get; set; }

        public byte Personality { get; set; }

        public byte Luck { get; set; }

        public byte[] Skills { get; set; }

        public byte Unknown1 { get; set; }

        public short Health { get; set; }

        public short SpellPts { get; set; }

        public short Fatigue { get; set; }

        public byte Disposition { get; set; }

        public byte Reputation { get; set; }

        public byte Rank { get; set; }

        public byte Unknown2 { get; set; }

        public int Gold { get; set; }

        public byte Unknown3 { get; set; }

        public NPDT()
        {
            Unknown1 = 0;
            Unknown2 = 0;
            Unknown3 = 0;
        }

        public NPDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            if(base.Size.Equals(52))
            {
                Level = reader.ReadBytes<short>(base.Data);
                Strength = reader.ReadBytes<byte>(base.Data);
                Intelligence = reader.ReadBytes<byte>(base.Data);
                Willpower = reader.ReadBytes<byte>(base.Data);
                Agility = reader.ReadBytes<byte>(base.Data);
                Speed = reader.ReadBytes<byte>(base.Data);
                Endurance = reader.ReadBytes<byte>(base.Data);
                Personality = reader.ReadBytes<byte>(base.Data);
                Luck = reader.ReadBytes<byte>(base.Data);
                Skills = reader.ReadBytes<byte[]>(base.Data, 27);
                Unknown1 = reader.ReadBytes<byte>(base.Data);
                Health = reader.ReadBytes<short>(base.Data);
                SpellPts = reader.ReadBytes<short>(base.Data);
                Fatigue = reader.ReadBytes<short>(base.Data);
                Disposition = reader.ReadBytes<byte>(base.Data);
                Reputation = reader.ReadBytes<byte>(base.Data);
                Rank = reader.ReadBytes<byte>(base.Data);
                Unknown2 = reader.ReadBytes<byte>(base.Data);
                Gold = reader.ReadBytes<int>(base.Data);
            }
            else //12 bytes, when stats are autocalculated?
            {
                Level = reader.ReadBytes<short>(base.Data);
                Disposition = reader.ReadBytes<byte>(base.Data);
                Reputation = reader.ReadBytes<byte>(base.Data);
                Rank = reader.ReadBytes<byte>(base.Data);
                Unknown1 = reader.ReadBytes<byte>(base.Data);
                Unknown2 = reader.ReadBytes<byte>(base.Data);
                Unknown3 = reader.ReadBytes<byte>(base.Data);
                Gold = reader.ReadBytes<int>(base.Data);
            }
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();
            if(Skills == null)
            {
                data.AddRange(ByteWriter.ToBytes(Level, typeof(short)));
                data.AddRange(ByteWriter.ToBytes(Disposition, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Reputation, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Rank, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Unknown1, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Unknown2, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Unknown3, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Gold, typeof(int)));
            }
            else
            {
                data.AddRange(ByteWriter.ToBytes(Level,typeof(short)));
                data.AddRange(ByteWriter.ToBytes(Strength, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Intelligence, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Willpower, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Agility, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Speed, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Endurance, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Personality, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Luck, typeof(byte)));
                foreach (byte skill in Skills)
                {
                    data.AddRange(ByteWriter.ToBytes(skill, typeof(byte)));
                }
                data.AddRange(ByteWriter.ToBytes(Unknown1, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Health, typeof(short)));
                data.AddRange(ByteWriter.ToBytes(SpellPts, typeof(short)));
                data.AddRange(ByteWriter.ToBytes(Fatigue, typeof(short)));
                data.AddRange(ByteWriter.ToBytes(Disposition, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Reputation, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Rank, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Unknown2, typeof(byte)));
                data.AddRange(ByteWriter.ToBytes(Gold, typeof(int)));
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }

        public void SetNPCSkill(Skill skill, int value) => Skills[(int)skill] = (byte)value;

        public int GetNPCSkill(Skill skill, int value) => Skills[(int)skill] = (byte)value;
    }
}
