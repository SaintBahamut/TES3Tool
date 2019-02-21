using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.ARMO
{
    /// <summary>
    /// Armour Data, required (24 bytes)
    /// </summary>
    public class AODT : Subrecord
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
        public int Type { get; set; }

        public float Weight { get; set; }

        public int Value { get; set; }

        public int Health { get; set; }

        public int EnchancmentPoints { get; set; }

        public int ArmorRating { get; set; }

        public AODT()
        {

        }

        public AODT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = reader.ReadBytes<int>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Health = reader.ReadBytes<int>(base.Data);
            EnchancmentPoints = reader.ReadBytes<int>(base.Data);
            ArmorRating = reader.ReadBytes<int>(base.Data);
        }
    }
}
