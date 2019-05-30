using TES3Lib.Base;
using TES3Lib.Subrecords.LAND;

namespace TES3Lib.Records
{
    /// <summary>
    /// Landscape Record
    /// </summary>
    public class LAND : Record
    {
        /// <summary>
        /// Coordinates
        /// </summary>
        public INTV INTV { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public DATA DATA { get; set; }

        /// <summary>
        /// Vertex normals
        /// </summary>
        public VNML VNML { get; set; }

        /// <summary>
        /// Vertex height map
        /// </summary>
        public VHGT VHGT { get; set; }

        /// <summary>
        /// Vertex low LOD height map
        /// </summary>
        public WNAM WNAM { get; set; }

        /// <summary>
        /// Vertex colors
        /// </summary>
        public VCLR VCLR { get; set; }

        /// <summary>
        /// Texture indices
        /// </summary>
        public VTEX VTEX { get; set; }

        public LAND(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
