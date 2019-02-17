using TES4Lib.Base;
using TES4Lib.Subrecords.LIGH;

namespace TES4Lib.Records
{
    public class LIGH : Record
    {
        public EDID EDID { get; set; }
        public FULL FULL { get; set; }
        public MODL MODL { get; set; }
        public MODB MODB { get; set; }
        public MODT MODT { get; set; }
        public SCRI SCRI { get; set; }
        public ICON ICON { get; set; }
        public DATA DATA { get; set; }
        public FNAM FNAM { get; set; }
        public SNAM SNAM { get; set; }

        public LIGH(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}