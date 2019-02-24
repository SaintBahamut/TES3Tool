using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.ARMO
{
    /// <summary>
    /// Body Part Index (1 byte)
    /// </summary>
    public class INDX : Subrecord
    {
        /// <summary>
        /// 0 = Head
		/// 1 = Hair
		/// 2 = Neck
		/// 3 = Cuirass
		/// 4 = Groin
		/// 5 = Skirt
		/// 6 = Right Hand
		/// 7 = Left Hand
		/// 8 = Right Wrist
		/// 9 = Left Wrist
		/// 10 = Shield
		/// 11 = Right Forearm
		/// 12 = Left Forearm
		/// 13 = Right Upper Arm
		/// 14 = Left Upper Arm
		/// 15 = Right Foot
		/// 16 = Left Foot
		/// 17 = Right Ankle
		/// 18 = Left Ankle
		/// 19 = Right Knee
		/// 20 = Left Knee
		/// 21 = Right Upper Leg
		/// 22 = Left Upper Leg
		/// 23 = Right Pauldron
		/// 24 = Left Pauldron
		/// 25 = Weapon
		/// 26 = Tail
        /// </summary>
        public byte Type { get; set; }

        public INDX()
        {

        }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = reader.ReadBytes<byte[]>(base.Data, base.Size)[0];
        }
    }
}
