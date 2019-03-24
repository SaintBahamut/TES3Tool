using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.CLOT
{
    /// <summary>
    /// Clothing Data, required (24 bytes)
    /// </summary>
    public class CTDT : Subrecord
    {
        /// <summary>
        /// 0 = Pants
        /// 1 = Shoes
        /// 2 = Shirt
        /// 3 = Belt
        /// 4 = Robe
        /// 5 = Right Glove
        /// 6 = Left Glove
        /// 7 = Skirt
        /// 8 = Ring
        /// 9 = Amulet
        /// </summary>
        public ClothingType Type { get; set; }

        public float Weight { get; set; }

        public short Value { get; set; }

        public short EnchancmentPoints { get; set; }

        public CTDT()
        {
        }

        public CTDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = reader.ReadBytes<ClothingType>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<short>(base.Data);
            EnchancmentPoints = reader.ReadBytes<short>(base.Data);
        }
    }
}
