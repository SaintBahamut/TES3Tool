using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.WEAP
{
    /// <summary>
    /// Weapon Data, 0x20 bytes binary, required
    /// </summary>
    public class WPDT : Subrecord
    {
        public float Weight { get; set; }
        public int Value { get; set; }

        /// <summary>
        /// Weapon type
        /// 0 = ShortBladeOneHand
		///	1 = LongBladeOneHand
		///	2 = LongBladeTwoClose
		///	3 = BluntOneHand
		///	4 = BluntTwoClose
		///	5 = BluntTwoWide
		///	6 = SpearTwoWide
		///	7 = AxeOneHand
		///	8 = AxeTwoHand
		///	9 = MarksmanBow
		///	10 = MarksmanCrossbow
		///	11 = MarksmanThrown
		///	12 = Arrow
		///	13 = Bolt
        /// </summary>
        public short Type { get; set; }
        public short Health { get; set; }
        public float Speed { get; set; }
        public float Reach { get; set; }
        public short EnchantpemntPoints { get; set; }
        public byte ChopMin { get; set; }
        public byte ChopMax { get; set; }
        public byte SlashMin { get; set; }
        public byte SlashMax { get; set; }
        public byte ThrustMin { get; set; }
        public byte ThrustMax { get; set; }

        /// <summary>
        /// Flags (0 to 1)
		///	0 = ?
		///	1 = Ignore Normal Weapon Resistance?
        /// </summary>
        public int Flags { get; set; }

        public WPDT()
        {

        }

        public WPDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Type = reader.ReadBytes<short>(base.Data);
            Health = reader.ReadBytes<short>(base.Data);
            Speed = reader.ReadBytes<float>(base.Data);
            Reach = reader.ReadBytes<float>(base.Data);
            EnchantpemntPoints = reader.ReadBytes<short>(base.Data);
            ChopMin = reader.ReadBytes<byte[]>(base.Data)[0];
            ChopMax = reader.ReadBytes<byte[]>(base.Data)[0];
            SlashMin = reader.ReadBytes<byte[]>(base.Data)[0];
            SlashMax = reader.ReadBytes<byte[]>(base.Data)[0];
            ThrustMin = reader.ReadBytes<byte[]>(base.Data)[0];
            ThrustMax = reader.ReadBytes<byte[]>(base.Data)[0];
            Flags = reader.ReadBytes<int>(base.Data);
        }
    }
}
