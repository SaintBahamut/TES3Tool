using TES3Lib.Base;
using TES3Lib.Subrecords.REGN;
using TES3Lib.Subrecords.Shared;
using SNAM = TES3Lib.Subrecords.REGN.SNAM;

namespace TES3Lib.Records
{
    public class REGN : Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public WEAT WEAT { get; set; }

        public BNAM BNAM { get; set; }

        public CNAM CNAM { get; set; }

        public SNAM SNAM { get; set; }

        public REGN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
