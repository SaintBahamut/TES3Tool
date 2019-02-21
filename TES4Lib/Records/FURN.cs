using TES4Lib.Base;
using TES4Lib.Subrecords.FURN;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class FURN : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }

        public SCRI SCRI { get; set; }

        public MNAM MNAM { get; set; }

        public FURN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}