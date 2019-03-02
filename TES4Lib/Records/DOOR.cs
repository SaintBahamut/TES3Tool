using TES4Lib.Base;
using TES4Lib.Subrecords.DOOR;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class DOOR : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }

        public SCRI SCRI { get; set; }

        public SNAM SNAM { get; set; }

        public ANAM ANAM { get; set; }

        public BNAM BNAM { get; set; }

        public FNAM FNAM { get; set; }

        public TNAM TNAM { get; set; }

        public DOOR(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}