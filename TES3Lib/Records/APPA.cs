using TES3Lib.Base;
using TES3Lib.Subrecords.APPA;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class APPA : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public AADT AADT { get; set; }

        public ITEX ITEX { get; set; }

        public SCRI SCRI { get; set; }

        public APPA()
        {
        }

        public APPA(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
