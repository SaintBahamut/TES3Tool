using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Subrecords.PGRD;

namespace TES4Lib.Records
{
    public class PGRD : Record
    {
        public DATA DATA { get; set; }

        public PGRP PGRP { get; set; }

        public PGAG PGAG { get; set; }

        public PGRR PGRR { get; set; }

        public PGRI PGRI { get; set; }

        public List<PGRL> PGRL { get; set; }

        public PGRD(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}