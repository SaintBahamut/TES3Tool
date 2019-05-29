using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.LTEX;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class LTEX : Record
    {
        public NAME NAME { get; set; }

        public INTV INTV { get; set; }

        public DATA DATA { get; set; }

        public LTEX(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
