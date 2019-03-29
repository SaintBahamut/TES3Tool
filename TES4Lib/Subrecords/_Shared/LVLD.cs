using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Chance none value
    /// </summary>
    public class LVLD : Subrecord
    {
        public byte ChanceNone { get; set; }

        public LVLD(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ChanceNone = reader.ReadBytes<byte>(base.Data);
        }
    }
}
