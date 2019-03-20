using TES4Lib.Base;
using TES4Lib.Subrecords.ARMO;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class ARMO : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public SCRI SCRI { get; set; }

        public BMDT BMDT { get; set; }

        public MODL MODL { get; set; }

        public MOD2 MOD2 { get; set; }

        public MOD3 MOD3 { get; set; }

        public MOD4 MOD4 { get; set; }

        public MODB MODB { get; set; }

        public MO2B MO2B { get; set; }

        public MO3B MO3B { get; set; }

        public MO4B MO4B { get; set; }

        public MODT MODT { get; set; }

        public MO2T MO2T { get; set; }

        public MO3T MO3T { get; set; }

        public MO4T MO4T { get; set; }

        public ICON ICON { get; set; }

        public ICO2 ICO2 { get; set; }

        public ANAM ANAM { get; set; }

        public ENAM ENAM { get; set; }

        public DATA DATA { get; set; }

        public ARMO(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}