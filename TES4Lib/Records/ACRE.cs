using TES4Lib.Base;
using TES4Lib.Subrecords.ACRE;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class ACRE : Record
    {
        public EDID EDID { get; set; }

        public NAME NAME { get; set; }

        public XRGD XRDG { get; set; }

        public XESP XESP { get; set; }

        public XOWN XOWN { get; set; }

        public XGLB XGLB { get; set; }

        public XRNK XRNK { get; set; }

        public XSCL XSCL { get; set; }

        public DATA DATA { get; set; }

        public ACRE(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}