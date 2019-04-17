using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REGN
{
    /// <summary>
    /// Map color
    /// </summary>
    public class RCLR : Subrecord
    {
        public uint Red { get; set; }

        public uint Green { get; set; }

        public uint Blue { get; set; }

        public byte Unused { get; set; }

        public RCLR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Red = reader.ReadBytes<uint>(this.Data);
            Green = reader.ReadBytes<uint>(this.Data);
            Blue = reader.ReadBytes<uint>(this.Data);
            Unused = reader.ReadBytes<byte>(this.Data);
        }
    }
}
