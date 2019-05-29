using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.BSGN;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class BSGN : Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public TNAM TNAM { get; set; }

        public DESC DESC { get; set; }

        public List<NPCS> NPCS { get; set; }

        public BSGN()
        {
        }

        public BSGN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
