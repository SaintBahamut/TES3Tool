using TES3Lib.Base;
using TES3Lib.Subrecords.LAND;

namespace TES3Lib.Records
{
    public class LAND : Record
    {
        public INTV INTV { get; set; }

        public DATA DATA { get; set; }

        public VNML VNML { get; set; }

        public VHGT VHGT { get; set; }

        public WNAM WNAM { get; set; }

        public VCLR VCLR { get; set; }

        public VTEX VTEX { get; set; }

        public LAND(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
