using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    public class HSND : Subrecord
    {
        public string HitSoundId { get; set; }

        public HSND(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            HitSoundId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}