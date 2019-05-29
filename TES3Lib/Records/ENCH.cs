using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.ENCH;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class ENCH : Record
    {
        public NAME NAME { get; set; }

        public ENDT ENDT { get; set; }

        public List<ENAM> ENAM { get; set; }

        public ENCH()
        {
        }

        public ENCH(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
