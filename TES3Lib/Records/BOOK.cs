using TES3Lib.Base;
using TES3Lib.Subrecords.BOOK;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    /// <summary>
    /// Books Record
    /// </summary>
    public class BOOK : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public BKDT BKDT { get; set; }

        public ITEX ITEX { get; set; }

        public SCRI SCRI { get; set; }

        public TEXT TEXT { get; set; }

        public ENAM ENAM { get; set; }

        public BOOK(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
