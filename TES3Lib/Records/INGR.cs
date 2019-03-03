using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.INGR;

namespace TES3Lib.Records
{
    public class INGR : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public IRDT IRDT { get; set; }

        public ITEX ITEX { get; set; }

        public SCRI SCRI { get; set; }

        public INGR(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
