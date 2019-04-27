using System.Collections.Generic;
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

        public List<SNAM> SNAM { get; set; }

        public INAM INAM { get; set; }

        public RNAM RNAM { get; set; }

        public List<SPLO> SPLO { get; set; }

        public SCRI SCRI { get; set; }

        public List<CNTO> CNTO { get; set; }

        public AIDT AIDT { get; set; }




        public NPC_(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}