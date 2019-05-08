using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.INFO;
using TES3Lib.Subrecords.NPC_;
using TES3Lib.Subrecords.Shared;
using NNAM = TES3Lib.Subrecords.INFO.NNAM;

namespace TES3Lib.Records
{
    /// <summary>
    /// Dialogue response record that
    /// belongs to previous DIAL record.
    /// </summary>
    public class INFO : Record
    {
        /// <summary>
        /// Info name string (unique sequence of #'s), Id
        /// </summary>
        public INAM INAM { get; set; }

        /// <summary>
        /// Previous info Id
        /// </summary>
        public PNAM PNAM { get; set; }

        /// <summary>
        /// Next info Id (form a linked list of INFOs for the DIAL).
        /// First INFO has an empty PNAM, last has an empty NNAM.
        /// </summary>
        public NNAM NNAM { get; set; }

        /// <summary>
        /// Info data
        /// </summary>
        public DATA DATA { get; set; }

        /// <summary>
        /// Actor Id
        /// </summary>
        public ONAM ONAM { get; set; }

        /// <summary>
        /// Race Id
        /// </summary>
        public RNAM RNAM { get; set; }

        /// <summary>
        /// Class Id
        /// </summary>
        public CNAM CNAM { get; set; }

        /// <summary>
        /// Faction Id
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Cell Id
        /// </summary>
        public ANAM ANAM { get; set; }

        /// <summary>
        /// PC Faction Id
        /// </summary>
        public DNAM DNAM { get; set; }

        /// <summary>
        /// The info response string (512 max)
        /// Not EditorId but text content
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Path for sound name if parent DIAL is Voice type
        /// </summary>
        public SNAM SNAM { get; set; }

        //Only one of QSTY,QSTF,QSTR can be set to true at same time
        /// <summary>
        /// Marks INFO entry as Quest name (Journal DIAL type)
        /// </summary>
        public QSTN QSTN { get; set; }

        /// <summary>
        /// Marks INFO entry as Quest name (Journal DIAL type)
        /// </summary>
        public QSTF QSTF { get; set; }

        /// <summary>
        /// Marks INFO entry as Restart (Journal DIAL type)
        /// </summary>
        public QSTR QSTR { get; set; }

        /// <summary>
        /// Function/variable choice (can hold up to 6 entries)
        /// </summary>
        public List<(SCVR function, INTV value )> SCVR { get; set; }

        /// <summary>
        /// Result text (not compiled)
        /// </summary>
        public BNAM BNAM { get; set; }


        public INFO(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }

        // TODO deserializer/serializer

 
    }
}
