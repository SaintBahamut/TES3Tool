using TES4Lib.Base;
using TES4Lib.Subrecords.NPC_;
using TES4Lib.Subrecords.Shared;
using SNAM = TES4Lib.Subrecords.NPC_.SNAM;

namespace TES4Lib.Records
{
    public class NPC_ : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public ACBS ACBS { get; set; }

        public SNAM SNAM { get; set; }


        public NPC_(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}