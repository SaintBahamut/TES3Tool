using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Static object record
    /// </summary>
    public class STAT : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public STAT()
        {
        }

        public STAT(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
