using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REGN
{
    public class WEAT : Subrecord
    {
        public byte Clear { get; set; }

        public byte Foggy { get; set; }

        public byte Overcast { get; set; }

        public byte Rain { get; set; }

        public byte Thunder { get; set; }

        public byte Ash { get; set; }

        public byte Blight { get; set; }

        public WEAT()
        {
        }

        public WEAT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Clear = reader.ReadBytes<byte>(base.Data);
            Foggy = reader.ReadBytes<byte>(base.Data);
            Overcast = reader.ReadBytes<byte>(base.Data);
            Rain = reader.ReadBytes<byte>(base.Data);
            Thunder = reader.ReadBytes<byte>(base.Data);
            Ash = reader.ReadBytes<byte>(base.Data);
            Blight = reader.ReadBytes<byte>(base.Data);
        }
    }
}
