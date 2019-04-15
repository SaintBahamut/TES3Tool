using TES4Lib.Base;
using TES4Lib.Subrecords.Shared;
using TES4Lib.Subrecords.WRLD;
using SNAM = TES4Lib.Subrecords.WRLD.SNAM;

namespace TES4Lib.Records
{
    /// <summary>
    /// A WRLD record defines a worldspace.
    /// A child worldspace inherits climate, water and map data from its parent.
    /// </summary>
    public class WRLD : Record
    {
        /// <summary>
        /// Worldspace editor ID
        /// </summary>
        public EDID EDID { get; set; }

        /// <summary>
        /// Worldspace name
        /// </summary>
        public FULL FULL { get; set; }

        /// <summary>
        /// Parent worldspace
        /// </summary>
        public WNAM WNAM { get; set; }

        /// <summary>
        /// Sound if not Default
        /// </summary>
        public SNAM SNAM { get; set; }

        /// <summary>
        /// Map filename
        /// </summary>
        public ICON ICON { get; set; }

        /// <summary>
        /// Climate if not NONE
        /// </summary>
        public CNAM CNAM { get; set; }

        /// <summary>
        /// Water
        /// </summary>
        public NAM2 NAM2 { get; set; }

        /// <summary>
        /// Map coordinates data
        /// </summary>
        public MNAM MNAM { get; set; }

        /// <summary>
        /// Flags
        /// </summary>
        public DATA DATA { get; set; }

        /// <summary>
        /// Coordinates for the bottom left corner of the worldspace
        /// </summary>
        public NAM0 NAM0 { get; set; }

        /// <summary>
        /// Coordinates for the top right corner of the worldspace
        /// </summary>
        public NAM9 NAM9 { get; set; }

        /// <summary>
        /// Its a mystery \o/
        /// </summary>
        public XXXX XXXX { get; set; }

        /// <summary>
        /// Offset data
        /// </summary>
        public OFST OFST { get; set; }


        public WRLD(byte[] rawData) : base(rawData)
        {  

            BuildSubrecords();
        }
    }
}