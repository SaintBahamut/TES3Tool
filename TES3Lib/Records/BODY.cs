using TES3Lib.Base;
using TES3Lib.Subrecords.BODY;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    public class BODY : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public BYDT BYDT { get; set; }

        public BODY()
        {
        }

        public BODY(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
