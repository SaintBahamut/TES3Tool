using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums;
using TES4Lib.Enums.Flags;
using Utility;

namespace TES4Lib.Subrecords.CLAS
{
    /// <summary>
    /// Various class data
    /// </summary>
    public class DATA : Subrecord
    {
        public ActorValue PrimaryAttribute1 { get; set; }

        public ActorValue PrimaryAttribute2 { get; set; }

        public Specialization Specialization { get; set; }

        public ActorValue[] MajorSkills { get; set; }

        public HashSet<ClassFlag> Flags { get; set; }

        public HashSet<ServicesFlag> Services { get; set; }

        public Skill SkillTrained { get; set; }

        public byte MaxTrainingLevel { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            PrimaryAttribute1 = (ActorValue)reader.ReadBytes<int>(base.Data);
            PrimaryAttribute2 = (ActorValue)reader.ReadBytes<int>(base.Data);
            Specialization = (Specialization)reader.ReadBytes<int>(base.Data);

            MajorSkills = new ActorValue[7];
            for (int i = 0; i < MajorSkills.Length; i++)
            {
                MajorSkills[i] = reader.ReadBytes<ActorValue>(base.Data);
            }

            Flags = reader.ReadFlagBytes<ClassFlag>(base.Data);
            Services = reader.ReadFlagBytes<ServicesFlag>(base.Data);

            if (base.Size > 48)
            {
                SkillTrained = reader.ReadBytes<Skill>(base.Data);
                MaxTrainingLevel = reader.ReadBytes<byte>(base.Data);
            }         
        }
    }
}
