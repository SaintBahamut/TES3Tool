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
        /// 0 = Helmet
		///	1 = Cuirass
		///	2 = L.Pauldron
		///	3 = R.Pauldron
		///	4 = Greaves
		///	5 = Boots
		///	6 = L.Gauntlet
		///	7 = R.Gauntlet
		///	8 = Shield
		///	9 = L.Bracer
		///	10 = R.Bracer
        /// </summary>
        public ClothingType Type { get; set; }

        public float Weight { get; set; }

        public int Value { get; set; }

        public int Health { get; set; }

        public int EnchancmentPoints { get; set; }

        public CTDT()
        {
        }

        public CTDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = reader.ReadBytes<ClothingType>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Health = reader.ReadBytes<int>(base.Data);
            EnchancmentPoints = reader.ReadBytes<int>(base.Data);
        }
    }
}
