using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.BOOK
{
    public class BKDT : Subrecord
    {
        public float Weight { get; set; }

        public int Value { get; set; }

        /// <summary>
        /// Scroll	(1 is scroll, 0 not)
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// SkillId of skill that book raises (-1 is no skill)
        /// </summary>
        public int SkillId { get; set; }

        public int EnchantPoints { get; set; }

        public BKDT()
        {

        }

        public BKDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Flag = reader.ReadBytes<int>(base.Data);
            SkillId = reader.ReadBytes<int>(base.Data);
            EnchantPoints = reader.ReadBytes<int>(base.Data);
        }
    }
}