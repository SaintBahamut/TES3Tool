using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.WRLD
{
    public class SNAM : Subrecord
    {
        public WorldSpaceMusicType MusicType { get; set; }

        public SNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MusicType = reader.ReadBytes<WorldSpaceMusicType>(base.Data);
        }
    }
}
