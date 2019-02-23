using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LIGH
{
    public class LHDT : Subrecord
    {
        public float Weight { get; set; }
        public int Value { get; set; }
        public int Time { get; set; }
        public int Radius { get; set; }
        
        /// <summary>
        /// RGB 1 byte per color with 4th unknown
        /// </summary>
        public int Color { get; set; }

        /// <summary>
        /// 0x0001 = Dynamic
		///	0x0002 = Can Carry
		///	0x0004 = Negative
		///	0x0008 = Flicker
		///	0x0010 = Fire
		///	0x0020 = Off Default
		///	0x0040 = Flicker Slow
		///	0x0080 = Pulse
		///	0x0100 = Pulse Slow
        /// </summary>
        public int Flags { get; set; }

        public LHDT()
        {

        }

        public LHDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Time = reader.ReadBytes<int>(base.Data);
            Radius = reader.ReadBytes<int>(base.Data);
            Color = reader.ReadBytes<int>(base.Data);
            Flags = reader.ReadBytes<int>(base.Data);
        }
    }
}