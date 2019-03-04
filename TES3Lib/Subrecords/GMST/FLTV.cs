using TES3Lib.Base;
using Utility;

/// <summary>
/// Float value (4 bytes)
/// </summary>

namespace TES3Lib.Subrecords.GMTS
{
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