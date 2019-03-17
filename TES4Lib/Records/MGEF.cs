using TES4Lib.Base;
using TES4Lib.Subrecords.MGEF;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class MGEF : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public DESC DESC { get; set; }

        public ICON ICON { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public DATA DATA { get; set; }

        public ESCE ESCE { get; set; }

        public MGEF(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}