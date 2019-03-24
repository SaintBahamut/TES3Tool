using TES4Lib.Base;
using TES4Lib.Subrecords.CELL;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class CELL : Record
    {
        public EDID EDID { get; set; }
        public FULL FULL { get; set; }
        public DATA DATA { get; set; }
        public XCLL XCLL { get; set; }
        public XCMT XCMT { get; set; }
        public XOWN XOWN { get; set; }
        public XGBL XGLB { get; set; }
        public XRNK XRNK { get; set; }
        public XCCM XCCM { get; set; }
        public XCWT XCWT { get; set; }
        public XCLW XCLW { get; set; }
        public XCLR XCLR { get; set; }
        public XCLC XCLC { get; set; }

        public CELL(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}