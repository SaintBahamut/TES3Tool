using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;
using Utility.Attributes;

namespace TES3Lib.Subrecords.SKILL
{
    public class INDX : Subrecord
    {
        public Skill Skill { get; }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Skill = reader.ReadBytes<Skill>(base.Data);
        }
    }
}