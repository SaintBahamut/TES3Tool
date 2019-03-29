using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Subrecords.LVLC;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class LVLC : Record
    {
        public EDID EDID { get; set; }

        public LVLD LVLD { get; set; }

        public LVLF LVLF { get; set; }

        public List<LVLO> LVLO { get; set; }

        public SCRI SCRI { get; set; }

        public TNAM TNAM { get; set; }

        public LVLC(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}