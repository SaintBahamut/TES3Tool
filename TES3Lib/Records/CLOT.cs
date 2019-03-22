using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.CLOT;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    public class CLOT : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public CTDT CTDT { get; set; }

        public ITEX ITEX { get; set; }

        public ENAM ENAM { get; set; }

        public List<(ITEX ITEX,BNAM BNAM, CNAM CNAM )> BPSL { get; set; }

        public SCRI SCRI { get; set; }

        public CLOT(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
