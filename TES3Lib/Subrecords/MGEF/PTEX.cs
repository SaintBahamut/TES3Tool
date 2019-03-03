using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    /// <summary>
    /// Particle texture path (32 characters max!)
    /// </summary>
    public class PTEX : Subrecord
    {
        public string ParticleTexturePath { get; set; }

        public PTEX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ParticleTexturePath = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
