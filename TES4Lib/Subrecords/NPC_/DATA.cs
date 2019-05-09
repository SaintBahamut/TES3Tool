using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// NPC stats
    /// </summary>
    public class DATA : Subrecord
    {
        public byte Armorer { get; set; }

        public byte Athletics { get; set; }

        public byte Blade { get; set; }

        public byte Block { get; set; }

        public byte Blunt { get; set; }

        public byte HandToHand { get; set; }

        public byte HeavyArmor { get; set; }

        public byte Alchemy { get; set; }

        public byte Alteration { get; set; }

        public byte Conjuration { get; set; }

        public byte Destruction { get; set; }

        public byte Illusion { get; set; }

        public byte Mysticism { get; set; }

        public byte Restoration { get; set; }

        public byte Acrobatics { get; set; }

        public byte LightArmor { get; set; }

        public byte Marksman { get; set; }

        public byte Mercantile { get; set; }

        public byte Security { get; set; }

        public byte Sneak { get; set; }

        public byte Speechcraft { get; set; }

        public ushort Health { get; set; }

        public byte Strength { get; set; }

        public byte Intelligence { get; set; }

        public byte Willpower { get; set; }

        public byte Agility { get; set; }

        public byte Speed { get; set; }

        public byte Endurance { get; set; }

        public byte Personality { get; set; }

        public byte Luck { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            Armorer= reader.ReadBytes<byte>(base.Data);
            Athletics = reader.ReadBytes<byte>(base.Data);
            Blade = reader.ReadBytes<byte>(base.Data);
            Block = reader.ReadBytes<byte>(base.Data);
            Blunt = reader.ReadBytes<byte>(base.Data);
            HandToHand = reader.ReadBytes<byte>(base.Data);
            HeavyArmor = reader.ReadBytes<byte>(base.Data);
            Alchemy = reader.ReadBytes<byte>(base.Data);
            Alteration = reader.ReadBytes<byte>(base.Data);
            Conjuration = reader.ReadBytes<byte>(base.Data);
            Destruction = reader.ReadBytes<byte>(base.Data);
            Illusion = reader.ReadBytes<byte>(base.Data);
            Mysticism = reader.ReadBytes<byte>(base.Data);
            Restoration = reader.ReadBytes<byte>(base.Data);
            Acrobatics = reader.ReadBytes<byte>(base.Data);
            LightArmor = reader.ReadBytes<byte>(base.Data);
            Marksman = reader.ReadBytes<byte>(base.Data);
            Mercantile = reader.ReadBytes<byte>(base.Data);
            Security = reader.ReadBytes<byte>(base.Data);
            Sneak = reader.ReadBytes<byte>(base.Data);
            Speechcraft = reader.ReadBytes<byte>(base.Data);
            Health = reader.ReadBytes<ushort>(base.Data);
            Strength = reader.ReadBytes<byte>(base.Data);
            Intelligence = reader.ReadBytes<byte>(base.Data);
            Willpower = reader.ReadBytes<byte>(base.Data);
            Agility = reader.ReadBytes<byte>(base.Data);
            Speed = reader.ReadBytes<byte>(base.Data);
            Endurance = reader.ReadBytes<byte>(base.Data);
            Personality = reader.ReadBytes<byte>(base.Data);
            Luck = reader.ReadBytes<byte>(base.Data);
        }
    }
}
