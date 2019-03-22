using TES4Lib.Base;
using TES4Lib.Subrecords.APPA;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class APPA : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public SCRI SCRI { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }

        public ICON ICON { get; set; }

        public DATA DATA { get; set; }

        public APPA(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}