using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.PROB
{
    /// <summary>
    /// Probe item details
    /// </summary>
    public class PBDT : Subrecord
    {
        public float Weight { get; set; }

        public int Value { get; set; }

        public int Uses { get; set; }

        public float Quality { get; set; }

        public PBDT()
        {
        }

        public PBDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Uses = reader.ReadBytes<int>(base.Data);
            Quality = reader.ReadBytes<float>(base.Data);
        }
    }
}
