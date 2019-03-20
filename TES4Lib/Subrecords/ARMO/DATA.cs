using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.ARMO
{

    public class DATA : Subrecord
    {
        /// <summary>
        /// Divide by 100 to get the actual in-game value.
        /// </summary>
        public short ArmorRating { get; set; }

        public int Value { get; set; }

        public int Health { get; set; }

        public float Weight { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ArmorRating = reader.ReadBytes<short>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Health = reader.ReadBytes<int>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
        }
    }
}
