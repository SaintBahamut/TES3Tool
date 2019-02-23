using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.SOUN
{
    public class FNAM : Subrecord
    {
        public string SoundFilename { get; set; }

        public FNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundFilename = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
