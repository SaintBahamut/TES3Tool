using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.APPA
{
    /// <summary>
    /// Hold information on an alchemy apparatus item.
    /// </summary>
    public class DATA : Subrecord
    {
        public ApparatusType Type { get; set; }

        public int Value { get; set; }

        public float Weight { get; set; }

        public float Quality { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = (ApparatusType)reader.ReadBytes<byte>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
            Quality = reader.ReadBytes<float>(base.Data);
        }
    }
}
