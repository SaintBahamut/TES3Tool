using TES3Lib.Base;
using TES3Lib.Subrecords.SNDG;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class SNDG : Record
    {
        public NAME NAME { get; set; }

        public DATA DATA { get; set; }

        public SNAM SNAM { get; set; }

        public CNAM CNAM { get; set; }

        public SNDG(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
