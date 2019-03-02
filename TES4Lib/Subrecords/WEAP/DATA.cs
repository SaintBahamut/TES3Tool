using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.WEAP
{
    /// <summary>
    /// Weapons data
    /// </summary>
    public class DATA : Subrecord
    {
        /// <summary>
        /// Type of weapon
        /// 0 = Blade One Hand
        /// 1 = Blade Two Hand
        /// 2 = Blunt One Hand
        /// 3 = Blunt Two Hand
        /// 4 = Staff
        /// 5 = Bow
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Weapon speed
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Weapon reach
        /// </summary>
        public float Reach { get; set; }

        /// <summary>
        /// Weapon flags
        /// 0x00000001 = Ignores Normal Weapon Resistance
        /// </summary>
        public int Flags { get; set; }

        /// <summary>
        /// Weapon health
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Weapon value
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Weapon weight
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Weapon damage
        /// </summary>
        public short Damage { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = reader.ReadBytes<int>(base.Data);
            Speed = reader.ReadBytes<float>(base.Data);
            Reach = reader.ReadBytes<float>(base.Data);
            Flags = reader.ReadBytes<int>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Health = reader.ReadBytes<int>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
            Damage = reader.ReadBytes<short>(base.Data);
        }
    }
}
