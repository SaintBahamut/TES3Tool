using TES4Lib.Base;
using TES4Lib.Subrecords.Shared;
using TES4Lib.Subrecords.SOUN;

namespace TES4Lib.Records
{
    public class SOUN : Record
    {
        public EDID EDID { get; set; }

        public FNAM FNAM { get; set; }

        public SNDD SNDD { get; set; }

        public SNDX SNDX { get; set; }


        public SOUN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}