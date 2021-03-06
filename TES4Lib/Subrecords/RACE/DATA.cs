﻿using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.RACE
{
    /// <summary>
    /// Various parameters of race
    /// </summary>
    public class DATA : Subrecord
    {
        public SkillBonus[] SkillBoosts { get; set; }

        public short Unknown { get; set; }

        public float MaleHeight { get; set; }

        public float FemaleHeight { get; set; }

        public float MaleWeight { get; set; }

        public float FemaleWeight { get; set; }

        public bool IsPlayable { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            SkillBoosts = new SkillBonus[7];

            for (int i = 0; i < 7; i++)
            {
                SkillBoosts[i].Skill = (ActorValue)reader.ReadBytes<byte>(base.Data);
                SkillBoosts[i].Bonus = reader.ReadBytes<byte>(base.Data);
            }

            Unknown = reader.ReadBytes<short>(base.Data);
            MaleHeight = reader.ReadBytes<float>(base.Data);
            FemaleHeight = reader.ReadBytes<float>(base.Data);
            MaleWeight = reader.ReadBytes<float>(base.Data);
            FemaleWeight = reader.ReadBytes<float>(base.Data);
            IsPlayable = reader.ReadBytes<bool>(base.Data);
        }

        public struct SkillBonus
        {
            public ActorValue Skill;
            public byte Bonus;
        }
    }
}
