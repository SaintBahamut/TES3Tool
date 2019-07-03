using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;
using Utility.Attributes;
using Attribute = TES3Lib.Enums.Attribute;

namespace TES3Lib.Subrecords.SPEL
{
    /// <summary>
    /// Spell data
    /// </summary>
    public class ENAM : Subrecord
    {
        [SizeInBytes(2)]
        public MagicEffect MagicEffect { get; set; }

        /// <summary>
        /// for skill related effects, 0xFFFFFFFF otherwise
        /// </summary>
        [SizeInBytes(1)]
        public Skill Skill { get; set; }

        /// <summary>
        /// for attribute related effects, 0xFFFFFFFF otherwise
        /// </summary>
        [SizeInBytes(1)]
        public Attribute Attribute { get; set; }

        public SpellRange SpellRange { get; set; }

        public int Area { get; set; }

        public int Duration { get; set; }

        public int MinMagnitude { get; set; }

        public int MaxMagnitude { get; set; }

        public ENAM()
        {
        }

        public ENAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MagicEffect = reader.ReadBytes<MagicEffect>(base.Data,2);
            Skill = reader.ReadBytes<Skill>(base.Data,1);
            Attribute = reader.ReadBytes<Attribute>(base.Data,1);
            SpellRange = reader.ReadBytes<SpellRange>(base.Data);
            Area = reader.ReadBytes<int>(base.Data);
            Duration = reader.ReadBytes<int>(base.Data);
            MinMagnitude = reader.ReadBytes<int>(base.Data);
            MaxMagnitude = reader.ReadBytes<int>(base.Data);
        }
    }
}
