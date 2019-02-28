using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.FLOR
{
    /// <summary>
    /// Marks production of ingredient at certain seasons
    /// </summary>
    public class PFPC : Subrecord
    {
        public byte SpringProd { get; set; }

        public byte SummerProd { get; set; }

        public byte FallProd { get; set; }

        public byte WinterProd { get; set; }

        public PFPC(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SpringProd = reader.ReadBytes<byte>(base.Data);
            SummerProd = reader.ReadBytes<byte>(base.Data);
            FallProd = reader.ReadBytes<byte>(base.Data);
            WinterProd = reader.ReadBytes<byte>(base.Data);
        }
    }
}
