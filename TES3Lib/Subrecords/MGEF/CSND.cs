using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    public class CSND : Subrecord
    {
        public string CastSoundId { get; set; }

        public CSND(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CastSoundId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}