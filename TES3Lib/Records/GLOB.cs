using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.GLOB;
using FNAM = TES3Lib.Subrecords.GLOB.FNAM;

namespace TES3Lib.Records
{
    /// <summary>
    /// Global variable record
    /// </summary>
    public class GLOB : Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public FLTV FLTV { get; set; }

        public GLOB()
        {
        }

        public GLOB(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
