using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.MGEF
{
    public class ASND : Subrecord
    {
        public string AreaSoundId { get; set; }

        public ASND(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            AreaSoundId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}