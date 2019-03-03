using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    public class BSND : Subrecord
    {
        public string BoldSoundId { get; set; }

        public BSND(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            BoldSoundId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}