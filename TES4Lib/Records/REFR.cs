using TES4Lib.Base;
using TES4Lib.Subrecords.REFR;

namespace TES4Lib.Records
{
    public class REFR : Record
    {
        public NAME NAME { get; set; }
        public EDID EDID { get; set; }
        public XMRK XMRK { get; set; }
        public FNAM FNAM { get; set; }
        public XOWN XOWN { get; set; }
        public XRNK XRNK { get; set; }
        public XGLB XGLB { get; set; }
        public XSCL XSCL { get; set; }
        public XTEL XTEL { get; set; }
        public XTRG XTRG { get; set; }
        public XSED XSED { get; set; }
        public XLOD XLOD { get; set; }
        public XPCI XPCI { get; set; }
        public XLOC XLOC { get; set; }
        public XESP XESP { get; set; }
        public XLCM XLCM { get; set; }
        public XRTM XRTM { get; set; }
        public XACT XACT { get; set; }
        public XCNT XCNT { get; set; }
        public FULL FULL { get; set; }
        public TNAM TNAM { get; set; }
        public ONAM ONAM { get; set; }
        public DATA DATA { get; set; }

        public REFR(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}