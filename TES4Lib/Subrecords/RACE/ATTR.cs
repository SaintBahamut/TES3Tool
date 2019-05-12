using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.RACE
{
    /// <summary>
    /// Base attributes
    /// </summary>
    public class ATTR : Subrecord
    {
        public Attributes Male { get; set; }

        public Attributes Female { get; set; }

        public ATTR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Male = new Attributes
            {
                Strength = reader.ReadBytes<byte>(base.Data),
                Intelligence = reader.ReadBytes<byte>(base.Data),
                Willpower = reader.ReadBytes<byte>(base.Data),
                Agility = reader.ReadBytes<byte>(base.Data),
                Speed = reader.ReadBytes<byte>(base.Data),
                Endurance = reader.ReadBytes<byte>(base.Data),
                Personality = reader.ReadBytes<byte>(base.Data),
                Luck = reader.ReadBytes<byte>(base.Data),
            };
            Female = new Attributes
            {
                Strength = reader.ReadBytes<byte>(base.Data),
                Intelligence = reader.ReadBytes<byte>(base.Data),
                Willpower = reader.ReadBytes<byte>(base.Data),
                Agility = reader.ReadBytes<byte>(base.Data),
                Speed = reader.ReadBytes<byte>(base.Data),
                Endurance = reader.ReadBytes<byte>(base.Data),
                Personality = reader.ReadBytes<byte>(base.Data),
                Luck = reader.ReadBytes<byte>(base.Data),
            };
        }

        public class Attributes
        {
            public byte Strength { get; set; }
            public byte Intelligence { get; set; }
            public byte Willpower { get; set; }
            public byte Agility { get; set; }
            public byte Speed { get; set; }
            public byte Endurance { get; set; }
            public byte Personality { get; set; }
            public byte Luck { get; set; }
        }
    }
}
