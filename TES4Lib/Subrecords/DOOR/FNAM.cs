using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.DOOR
{
    /// <summary>
    /// Door flags
    /// </summary>
    public class FNAM : Subrecord
    {
        /// <summary>
        /// Flags
        /// 0x01 = Oblivion gate
        /// 0x02 = Automatic door
        /// 0x04 = Hidden
        /// 0x08 = Minimal use
        /// </summary>
        public byte Flags { get; set; }

        public FNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadBytes<byte>(base.Data);
        }
    }
}
