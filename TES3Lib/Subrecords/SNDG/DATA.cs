using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.SNDG
{
    /// <summary>
    /// Sound gen tpye
    /// </summary>
    public class DATA : Subrecord
    {
        public SoundGenType SoundType { get; set; }

        public DATA()
        {
        }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundType = reader.ReadBytes<SoundGenType>(base.Data);
        }
    }
}
