using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.CLAS;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class CLAS: Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public CLDT CLDT { get; set; }

        public DESC DESC { get; set; }

        public CLAS()
        {
        }

        public CLAS(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }

        //public override string GetEditorId()
        //{
        //    return !IsNull(NAME) ? NAME.EditorId : null;
        //}
    }
}
