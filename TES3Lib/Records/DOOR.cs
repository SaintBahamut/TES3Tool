using TES3Lib.Base;
using TES3Lib.Subrecords.DOOR;

namespace TES3Lib.Records
{
    public class DOOR : Record
    {
        public NAME NAME { get; set; }
        public FNAM FNAM { get; set; }
        public MODL MODL { get; set; }
        public SCIP SCIP { get; set; }
        public SNAM SNAM { get; set; }
        public ANAM ANAM { get; set; }

        public DOOR()
        {
            Flags = 0;
            Header = 0;
        }

        public DOOR(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
