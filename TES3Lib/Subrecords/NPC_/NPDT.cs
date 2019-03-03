using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class NPDT : Subrecord
    {
        public short Level { get; set; }

        public byte Strength { get; set; }

        public byte Willpower { get; set; }

        public byte Agility { get; set; }

        public byte Speed { get; set; }

        public byte Endurance { get; set; }

        public byte Personality { get; set; }

        public byte Luck { get; set; }

        public byte[] Skills { get; set; }

        public byte Reputation { get; set; }

        public short Health { get; set; }

        public short SpellPts { get; set; }

        public short Fatigue { get; set; }

        public byte Disposition { get; set; }

        public byte FactionId { get; set; }

        public byte Rank { get; set; }

        public byte Unknown1 { get; set; }

        public int Gold { get; set; }

        public NPDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Level = reader.ReadBytes<short>(base.Data);
            Strength = reader.ReadBytes<byte>(base.Data);
            Willpower = reader.ReadBytes<byte>(base.Data);
            Agility = reader.ReadBytes<byte>(base.Data);
            Speed = reader.ReadBytes<byte>(base.Data);
            Endurance = reader.ReadBytes<byte>(base.Data);
            Personality = reader.ReadBytes<byte>(base.Data);
            Luck = reader.ReadBytes<byte>(base.Data);
            Skills = reader.ReadBytes<byte[]>(base.Data, 27);
            Reputation = reader.ReadBytes<byte>(base.Data);
		    Health = reader.ReadBytes<short>(base.Data);
            SpellPts = reader.ReadBytes<short>(base.Data);
            Fatigue = reader.ReadBytes<short>(base.Data);
            Disposition = reader.ReadBytes<byte>(base.Data);
            FactionId = reader.ReadBytes<byte>(base.Data);
            Rank = reader.ReadBytes<byte>(base.Data);
            Unknown1 = reader.ReadBytes<byte>(base.Data);
            Gold = reader.ReadBytes<int>(base.Data);
        }

        public void SetNPCSkill(Skill skill, int value) => Skills[(int)skill] = (byte)value;

        public int GetNPCSkill(Skill skill, int value) => Skills[(int)skill] = (byte)value;
    }
}
