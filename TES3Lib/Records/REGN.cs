using TES3Lib.Base;
using TES3Lib.Subrecords.REGN;
using TES3Lib.Subrecords.Shared;
using SNAM = TES3Lib.Subrecords.REGN.SNAM;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class REGN : Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public WEAT WEAT { get; set; }

        /// <summary>
        /// Id of creature that wokes player when resting
        /// </summary>
        public BNAM BNAM { get; set; }

        public CNAM CNAM { get; set; }

        public SNAM SNAM { get; set; }

        public REGN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
