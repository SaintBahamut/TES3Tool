using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.ENCH;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    public class ENCH : Record
    {
        public NAME NAME { get; set; }

        public ENDT ENDT { get; set; }

        public List<ENAM> ENAM { get; set; }

        public ENCH()
        {
        }

        public ENCH(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
