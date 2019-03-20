using TES4Lib.Base;
using TES4Lib.Subrecords.CREA;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class CREA : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }

        public NIFZ NIFZ { get; set; }

        public ACBS ACBS { get; set; }

        public SNAM SNAM { get; set; }

        public INAM INAM { get; set; }

        public RNAM RNAM { get; set; }

        public SPLO SPLO { get; set; }

        public SCRI SCRI { get; set; }

        public CNTO CNTO { get; set; }

        public AIDT AIDT { get; set; }

        public PKID PKID { get; set; }

        public DATA DATA { get; set; }

        public ZNAM ZNAM { get; set; }

        public CSCR CSCR { get; set; }

        public CSDT CSDT { get; set; }

        public CSDI CSDI { get; set; }

        public CSDC CSDC { get; set; }

        public BNAM BNAM { get; set; }

        public TNAM TNAM { get; set; }

        public WNAM WNAM { get; set; }

        public NAM0 NAM0 { get; set; }

        public NAM1 NAM1 { get; set; }

        public KFFZ KFFZ { get; set; }

        public CREA(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}