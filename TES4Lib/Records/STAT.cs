using TES4Lib.Base;
using TES4Lib.Subrecords.STAT;

namespace TES4Lib.Records
{
    public class STAT : Record
    {
        public EDID EDID { get; set; }
        public MODL MODL { get; set; }
        public MODB MODB { get; set; }
        public MODT MODT { get; set; }

        public STAT(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}