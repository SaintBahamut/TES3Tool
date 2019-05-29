using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.LIGH;


namespace TES3Lib.Records
{
    /// <summary>
    /// Record for light emiting objects
    /// </summary>
    public class LIGH : Record
    {
        public NAME NAME { get; set; }
        public FNAM FNAM { get; set; }
        public LHDT LHDT { get; set; }
        public SCPT SCPT { get; set; }
        public ITEX ITEX { get; set; }
        public MODL MODL { get; set; }
        public SNAM SNAM { get; set; }
        public SCRI SCRI { get; set; }

        public LIGH()
        {
        }

        public LIGH(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
