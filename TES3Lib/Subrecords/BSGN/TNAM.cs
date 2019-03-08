using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.BSGN
{
    /// <summary>
    /// Birthsign graphic texture path
    /// </summary>
    public class TNAM : Subrecord
    {
        public string TexturePath { get; set; }

        public TNAM()
        {
        }

        public TNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            TexturePath = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
