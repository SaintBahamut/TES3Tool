using TES3Lib.Base;
using TES3Lib.Subrecords.LOCK;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class LOCK : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public LKDT LKDT { get; set; }

        public ITEX ITEX { get; set; }

        public SCRI SCRI { get; set; }

        public LOCK(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
