using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.CLAS;

namespace TES3Lib.Records
{
    public class CLAS: Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Attributes and skills
        /// </summary>
        public CLDT CLDT { get; set; }

        /// <summary>
        /// Description
        /// </summary>
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

    }
}
