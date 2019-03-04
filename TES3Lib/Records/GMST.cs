using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.GMTS;

namespace TES3Lib.Records
{
    /// <summary>
    /// Game settings record
    /// </summary>
    public class GMST : Record
    {
        public NAME NAME { get; set; }

        public STRV STRV { get; set; }

        public INTV INTV { get; set; }

        public FLTV FLTV { get; set; }

        public GMST()
        {
        }

        public GMST(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
