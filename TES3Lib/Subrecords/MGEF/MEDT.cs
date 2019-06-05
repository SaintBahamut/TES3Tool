using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    public class MEDT : Subrecord
    {
        public  SpellSchool SpellSchool { get; set; }

        public float BaseCost { get; set; }

        /// <summary>
        /// 0x0200 = Spellmaking
		///	0x0400 = Enchanting
		///	0x0800 = Negative
        /// </summary>
        public int Flags { get; set; }

        public int Red { get; set; }

        public int Green { get; set; }

        public int Blue { get; set; }

        public float SpeedMultiplier { get; set; }

        public float SizeMultiplier { get; set; }

        public float SizeCap { get; set; }

        public MEDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SpellSchool = (SpellSchool)reader.ReadBytes<int>(base.Data);
            BaseCost = reader.ReadBytes<float>(base.Data);
            Flags = reader.ReadBytes<int>(base.Data);
            Red = reader.ReadBytes<int>(base.Data); 
            Green = reader.ReadBytes<int>(base.Data);
            Blue = reader.ReadBytes<int>(base.Data);
            SpeedMultiplier = reader.ReadBytes<float>(base.Data);
            SizeMultiplier = reader.ReadBytes<float>(base.Data);
            SizeCap = reader.ReadBytes<float>(base.Data);
        }
    }
}