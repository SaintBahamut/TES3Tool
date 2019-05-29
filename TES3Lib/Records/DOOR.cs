using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class DOOR : Record
    {
        public NAME NAME { get; set; }
        public MODL MODL { get; set; }
        public FNAM FNAM { get; set; }
        public SCRI SCRI { get; set; }
        public SCIP SCIP { get; set; }

        /// <summary>
        /// Door open sound
        /// </summary>
        public SNAM SNAM { get; set; }

        /// <summary>
        /// Door close sound
        /// </summary>
        public ANAM ANAM { get; set; }

        public DOOR()
        {
        }

        public DOOR(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
