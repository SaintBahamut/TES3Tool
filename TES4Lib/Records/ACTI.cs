using TES4Lib.Base;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class ACTI : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }

        public SCRI SCRI { get; set; }

        public SNAM SNAM { get; set; }

        public ACTI(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}