using TES3Lib.Base;
using TES3Lib.Subrecords.PGRD;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class PGRD : Record
    {
        public DATA DATA { get; set; }

        /// <summary>
        /// Cell or region name
        /// </summary>
        public NAME NAME { get; set; }

        public PGRP PGRP { get; set; }

        public PGRC PGRC { get; set; }

        public PGRD()
        {
        }

        public PGRD(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
