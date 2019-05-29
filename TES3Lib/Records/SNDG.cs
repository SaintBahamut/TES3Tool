using TES3Lib.Base;
using TES3Lib.Subrecords.SNDG;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    public class SNDG : Record
    {
        public NAME NAME { get; set; }

        public DATA DATA { get; set; }

        public SNAM SNAM { get; set; }

        public CNAM CAME { get; set; }

        public SNDG(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
