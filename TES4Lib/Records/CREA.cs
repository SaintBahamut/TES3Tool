using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Subrecords.CREA;
using TES4Lib.Subrecords.Shared;
using BNAM = TES4Lib.Subrecords.CREA.BNAM;
using SNAM = TES4Lib.Subrecords.CREA.SNAM;

namespace TES4Lib.Records
{
    public class CREA : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        /// <summary>
        /// Skeletal NIF-filename with directory
        /// </summary>
        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }

        public NIFZ NIFZ { get; set; }

        public NIFT NIFT { get; set; }

        public ACBS ACBS { get; set; }

        public List<SNAM> SNAM { get; set; }

        public INAM INAM { get; set; }

        public RNAM RNAM { get; set; }

        /// <summary>
        /// Creatures spells
        /// </summary>
        public List<SPLO> SPLO { get; set; }

        public SCRI SCRI { get; set; }

        /// <summary>
        /// Creatures items
        /// </summary>
        public List<CNTO> CNTO { get; set; }

        public AIDT AIDT { get; set; }

        public List<PKID> PKID { get; set; }

        public DATA DATA { get; set; }

        public ZNAM ZNAM { get; set; }

        public CSCR CSCR { get; set; }

        #region should make these 3 one list but i dont really need those

        public List<CSDT> CSDT { get; set; }

        public List<CSDI> CSDI { get; set; }

        public List<CSDC> CSDC { get; set; }

        #endregion

        public BNAM BNAM { get; set; }

        public TNAM TNAM { get; set; }

        public WNAM WNAM { get; set; }

        public NAM0 NAM0 { get; set; }

        public NAM1 NAM1 { get; set; }

        /// <summary>
        /// Optional Animation List for Creature
        /// </summary>
        public KFFZ KFFZ { get; set; }

        public CREA(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}