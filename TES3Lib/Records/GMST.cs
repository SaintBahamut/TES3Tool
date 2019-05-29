using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.GMTS;
using static Utility.Common;

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

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
