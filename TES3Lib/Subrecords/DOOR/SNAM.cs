using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.DOOR
{
    public class SNAM : Subrecord
    {
        public string SoundNameClose { get; set; }

        public SNAM()
        {

        }

        public SNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundNameClose = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
