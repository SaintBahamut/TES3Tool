using TES4Lib.Base;
using TES4Lib.Enums.Flags;
using Utility;

namespace TES4Lib.Subrecords.AMMO
{
    /// <summary>
    /// Ammo data
    /// </summary>
    public class DATA : Subrecord
    {
        public float Speed { get; set; }

        public AmmoFlag Flags { get; set; }

        public int Value { get; set; }

        public float Weight { get; set; }

        public short Damage { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Speed = reader.ReadBytes<float>(base.Data);
            Flags = reader.ReadBytes<AmmoFlag>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
            Damage = reader.ReadBytes<short>(base.Data);
        }
    }
}
