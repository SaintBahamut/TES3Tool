using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.CONT
{
    /// <summary>
    /// Container weight
    /// </summary>
    public class CNDT : Subrecord
    {
        public float Weight { get; set; }

        public CNDT()
        {

        }

        public CNDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Weight = reader.ReadBytes<float>(base.Data);
        }
    }
}
