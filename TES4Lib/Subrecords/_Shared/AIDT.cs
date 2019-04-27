using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums;
using TES4Lib.Enums.Flags;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    public class AIDT : Subrecord
    {
        public byte Aggression { get; set; }

        public byte Confidence { get; set; }

        public byte EnergyLevel { get; set; }

        public byte Responsibility { get; set; }

        public HashSet<ServicesFlag> Services { get; set; }

        public Skill TrainedSkill { get; set; }

        public byte TrainingLevel { get; set; }

        public ushort Unused { get; set; }

        public AIDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Aggression = reader.ReadBytes<byte>(base.Data);
            Confidence = reader.ReadBytes<byte>(base.Data);
            EnergyLevel = reader.ReadBytes<byte>(base.Data);
            Responsibility = reader.ReadBytes<byte>(base.Data);
            Services = reader.ReadFlagBytes<ServicesFlag>(base.Data);
            TrainedSkill = reader.ReadBytes<Skill>(base.Data);
            TrainingLevel = reader.ReadBytes<byte>(base.Data);
            Unused = reader.ReadBytes<ushort>(base.Data);
        }
    }
}
