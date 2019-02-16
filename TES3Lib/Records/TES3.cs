using TES3Lib.Structures.Base;
using TES3Lib.Subrecords.TES3;

namespace TES3Lib.Records
{
    public class TES3 : Record
    {
        public HEDR HEDR { get; set; }

        public MAST MAST { get; set; }

        public DATA DATA { get; set; }

        public TES3()
        {
            base.Flags = 0;
            base.Header = 0;
            HEDR = new HEDR();
            MAST = new MAST();
            DATA = new DATA();
        }

        public TES3(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
