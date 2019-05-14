using TES4Lib.Base;
using TES4Lib.Subrecords.CLAS;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class CLAS : Record
    {
        /// <summary>
        /// Class EditorId
        /// </summary>
        public EDID EDID { get; set; }

        /// <summary>
        /// Class display name
        /// </summary>
        public FULL FULL { get; set; }

        /// <summary>
        /// Class description
        /// </summary>
        public DESC DESC { get; set; }

        /// <summary>
        /// Class graphics image
        /// </summary>
        public ICON ICON { get; set; }

        /// <summary>
        /// Various class data
        /// </summary>
        public DATA DATA { get; set; }

        public CLAS(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}