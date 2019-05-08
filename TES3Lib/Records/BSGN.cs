using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.BSGN;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    public class BSGN : Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public TNAM TNAM { get; set; }

        public DESC DESC { get; set; }

        public List<NPCS> NPCS { get; set; }

        public BSGN()
        {
        }

        public BSGN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
