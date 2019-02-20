using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MISC
{
    /// <summary>
    /// Item parameters
    /// </summary>
    public class MCDT : Subrecord
    {
        public float Weight { get; set; }
        public int Value { get; set; }
        public int Unknown { get; set; }

        public MCDT()
        {

        }

        public MCDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Weight = reader.ReadBytes<float>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Unknown = reader.ReadBytes<int>(base.Data);
        }
    }
}
