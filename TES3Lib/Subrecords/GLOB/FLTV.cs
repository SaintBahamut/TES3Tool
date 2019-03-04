using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.GLOB
{
    /// <summary>
    /// Float value (4 bytes)
    /// </summary>
    public class FLTV : Subrecord
    {
        public float FloatValue { get; set; }

        public FLTV()
        {
        }

        public FLTV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FloatValue = reader.ReadBytes<float>(base.Data);
        }
    }
}