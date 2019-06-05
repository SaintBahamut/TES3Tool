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
        /// <summary>
        /// Serialized to data as a 4-byte value.
        /// </summary>
        [SizeInBytes(4)]
        public Skill Skill { get; }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Skill = (Skill)reader.ReadBytes<int>(base.Data);
        }
    }
}