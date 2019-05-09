using System.Collections.Generic;
using System.Diagnostics;
using TES4Lib.Base;
using TES4Lib.Subrecords.NPC_;
using TES4Lib.Subrecords.Shared;
using ENAM = TES4Lib.Subrecords.NPC_.ENAM;
using SNAM = TES4Lib.Subrecords.NPC_.SNAM;

namespace TES4Lib.Records
{
    /// <summary>
    /// NPC Record
    /// </summary>
    [DebuggerDisplay("{EDID.EditorId}")]
    public class NPC_ : Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public ACBS ACBS { get; set; }

        public List<SNAM> SNAM { get; set; }

        public INAM INAM { get; set; }

        public RNAM RNAM { get; set; }

        public List<SPLO> SPLO { get; set; }

        public SCRI SCRI { get; set; }

        public List<CNTO> CNTO { get; set; }

        public AIDT AIDT { get; set; }

        /// <summary>
        /// FormId of referenced AI Package list
        /// </summary>
        public List<PKID> PKID { get; set; }

        /// <summary>
        /// FormId of referenced NPC Class
        /// </summary>
        public CNAM CNAM { get; set; }

        /// <summary>
        /// NPC stats
        /// </summary>
        public DATA DATA { get; set; }

        /// <summary>
        /// FormId of referenced HAIR record
        /// </summary>
        public HNAM HNAM { get; set; }

        /// <summary>
        /// Hair length
        /// </summary>
        public LNAM LNAM { get; set; }

        /// <summary>
        /// FormId of referenced EYES record
        /// </summary>
        public ENAM ENAM { get; set; }

        /// <summary>
        /// Hair color
        /// </summary>
        public HCLR HCLR { get; set; }

        /// <summary>
        /// FormId of referenced CSTY record
        /// </summary>
        public ZNAM ZNAM { get; set; }

        /// <summary>
        /// FaceGen Geometry-Symmetric
        /// </summary>
        public FGGS FGGS { get; set; }

        /// <summary>
        /// FaceGen Geometry-Asymmetric
        /// </summary>
        public FGGA FGGA { get; set; }

        /// <summary>
        /// FaceGen Texture-Symmetic
        /// </summary>
        public FGTS FGTS { get; set; }

        /// <summary>
        /// Unknown, but is a fixed value for every race. 
        /// Does not get updated unless you visit the face tab in the editor.
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Optional Animation List for NPC
        /// </summary>
        public KFFZ KFFZ { get; set; }

        public NPC_(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}