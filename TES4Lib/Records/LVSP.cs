using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class LVSP : Record
    {
        public EDID EDID { get; set; }

        public LVLD LVLD { get; set; }

        public LVLF LVLF { get; set; }

        public List<LVLO> LVLO { get; set; }

        public LVSP(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}