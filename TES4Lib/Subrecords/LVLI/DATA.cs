using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.LVLI
{
    /// <summary>
    /// This 1 byte (confirmed) older format subrecord stores the Calculate for Each Item in Count flag.
    /// Any value other than 0x00 = Calculate for Each Item in Count
    /// </summary>
    public class DATA : Subrecord
    {
        /// <summary>
        /// Any value other than 0x00 = Calculate for Each Item in Count
        /// </summary>
        public byte Flags { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadBytes<byte>(base.Data);
        }
    }
}
