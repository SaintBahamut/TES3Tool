using TES4Lib.Base;
using TES4Lib.Subrecords.FLOR;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class FLOR : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public SCRI SCRI { get; set; }

        public PFIG PFIG { get; set; }

        public PFPC PFPC { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }


        public FLOR(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}