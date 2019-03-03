using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.MGEF;

namespace TES3Lib.Records
{
    public class MGEF : Record
    {
        public INDX INDX { get; set; }

        public MEDT MEDT { get; set; }

        public ITEX ITEX { get; set; }

        public PTEX PTEX { get; set; }

        public CVFX CVFX { get; set; }

        public BVFX BVFX { get; set; }

        public HVFX HVFX { get; set; }

        public AVFX AVFX { get; set; }

        public DESC DESC { get; set; }

        public CSND CSND { get; set; }

        public BSND BSND { get; set; }

        public HSND HSND { get; set; }

        public ASND ASND { get; set; }

        public MGEF(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
