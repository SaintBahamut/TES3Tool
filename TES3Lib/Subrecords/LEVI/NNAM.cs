using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LEVI
{
    public class NNAM : Subrecord
    {
        /// <summary>
        /// 1 or 0 ?
        /// </summary>
        public byte ChanceNone { get; set; }

        public NNAM()
        {
        }

        public NNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ChanceNone = reader.ReadBytes<byte>(base.Data);
        }
    }
}
