using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;
using Attribute = TES3Lib.Enums.Attribute;

namespace TES3Lib.Subrecords.ALCH
{
    /// <summary>
    /// Alchemy data
    /// </summary>
    public class ENAM : Subrecord
    {
        public MagicEffect MagicEffect { get; set; }

        /// <summary>
        /// for skill related effects, -1/0 otherwise
        /// </summary>
        public Skill Skill { get; set; }

        /// <summary>
        /// for attribute related effects, -1/0 otherwise
        /// </summary>
        public Attribute Attribute { get; set; }

        public int Unknown1 { get; set; }

        public int Unknown2 { get; set; }

        public int Duration { get; set; }

        public int Unknown3 { get; set; }

        public int Magnitude { get; set; }

        public ENAM()
        {
        }

        public ENAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MagicEffect = reader.ReadBytes<MagicEffect>(base.Data);
            Skill = reader.ReadBytes<Skill>(base.Data);
            Attribute = reader.ReadBytes<Attribute>(base.Data);
            Unknown1 = reader.ReadBytes<int>(base.Data);
            Unknown2 = reader.ReadBytes<int>(base.Data);
            Duration = reader.ReadBytes<int>(base.Data);
            Unknown3 = reader.ReadBytes<int>(base.Data);
            Magnitude = reader.ReadBytes<int>(base.Data);
        }
    }
}
