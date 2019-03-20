using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.LVLI
{
    /// <summary>
    /// Chance none value
    /// </summary>
    public class LVLD : Subrecord
    {
        public byte ChanceNoneValue { get; set; }

        public LVLD(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ChanceNoneValue = reader.ReadBytes<byte>(base.Data);
        }
    }
}
