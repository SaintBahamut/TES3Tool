using TES4Lib.Base;
using TES4Lib.Subrecords.ACHR;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class ACHR : Record
    {
        public EDID EDID { get; set; }

        public NAME NAME { get; set; }

        public XRGD XRGD { get; set; }

        public XESP XESP { get; set; }

        public XHRS XHRS { get; set; }

        public XMRC XMRC { get; set; }

        public XSCL XSCL { get; set; }

        public DATA DATA { get; set; }

        public ACHR(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}