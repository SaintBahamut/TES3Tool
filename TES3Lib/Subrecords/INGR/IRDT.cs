using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.INGR
{
    public class IRDT : Subrecord
    {
        public float Weight { get; set; }

        public int Value { get; set; }

        public int[] EffectIds { get; set; }

        public int[] SkillsIds { get; set; }

        public int[] AttributeId { get; set; }

        public IRDT()
        {

        }

        public IRDT(byte[] rawData):base(rawData)
        {
           var reader = new ByteReader();

            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);

        }    
    }
}
