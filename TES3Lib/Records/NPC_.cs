using TES3Lib.Base;
using TES3Lib.Subrecords.NPC_;

namespace TES3Lib.Records
{
    public class NPC_ : Record
    {
        public NAME NAME { get; set; }
        public FNAM FNAM { get; set; }
        public MODL MODL { get; set; }
        public RNAM RNAM { get; set; }
        public ANAM ANAM { get; set; }
        public BNAM BNAM { get; set; }
        public CNAM CNAM { get; set; }
        public KNAM KNAM { get; set; }
        public NPDT NPDT { get; set; }
        public FLAG FLAG { get; set; }
        public NPCO NPCO { get; set; }
        public NPCS NPCS { get; set; }
        public AIDT AIDT { get; set; }
        public AI_W AI_W { get; set; }
        public AI_T AI_T { get; set; }
        public AI_F AI_F { get; set; }
        public AI_E AI_E { get; set; }
        public CNDT CNDT { get; set; }
        public AI_A AI_A { get; set; }
        public DODT DODT { get; set; }
        public DNAM DNAM { get; set; }
        public XSCL XSCL { get; set; }

        public NPC_(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
            IsImplemented = false;
        }
    }
}
