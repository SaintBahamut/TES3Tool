using System.Diagnostics;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// Hair color
    /// </summary>
    [DebuggerDisplay("Hair color: R{Red}:G{Green}:B{Blue}")]
    public class HCLR : Subrecord
    {
        public byte Red { get; set; }

        public byte Green { get; set; }

        public byte Blue { get; set; }

        public byte CustomFlag { get; set; }

        public HCLR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            Red = reader.ReadBytes<byte>(base.Data);
            Green = reader.ReadBytes<byte>(base.Data);
            Blue = reader.ReadBytes<byte>(base.Data);
            CustomFlag = reader.ReadBytes<byte>(base.Data);
        }
    }
}
