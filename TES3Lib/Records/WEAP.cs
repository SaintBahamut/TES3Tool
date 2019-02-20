using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.WEAP;

namespace TES3Lib.Records
{
    /// <summary>
    /// Weapon record
    /// </summary>
    public class WEAP : Record
    {
        public NAME NAME { get; set; }
        public MODL MODL { get; set; }
        public FNAM FNAM { get; set; }
        public WPDT WPDT { get; set; }
        public ITEX ITEX { get; set; }
        public ENAM ENAM { get; set; }
        public SCRI SCRI { get; set; }

        public WEAP()
        {

        }

        public WEAP(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
