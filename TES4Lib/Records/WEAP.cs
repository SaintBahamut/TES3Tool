using TES4Lib.Base;
using TES4Lib.Subrecords.Shared;
using TES4Lib.Subrecords.WEAP;

namespace TES4Lib.Records
{
    public class WEAP : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public SCRI SCRI { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }

        public ICON ICON { get; set; }

        public ANAM ANAM { get; set; }

        public ENAM ENAM { get; set; }

        public DATA DATA { get; set; }

        public WEAP(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}