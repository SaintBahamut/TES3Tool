using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.CELL
{
    /// <summary>
    /// Lighting color of interior cell
    /// </summary>
    public class AMBI : Subrecord
    {
        public int AmbientColor { get; set; }

        public int SunlightColor { get; set; }

        public int FogColor { get; set; }

        public float FogDensity { get; set; }

        public AMBI()
        {

        }

        public AMBI(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            AmbientColor = reader.ReadBytes<int>(base.Data);
            SunlightColor = reader.ReadBytes<int>(base.Data);
            FogColor = reader.ReadBytes<int>(base.Data);
            FogDensity = reader.ReadBytes<float>(base.Data);
        }
    }
}
