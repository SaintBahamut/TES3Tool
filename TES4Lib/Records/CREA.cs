using System;
using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Subrecords.CREA;
using TES4Lib.Subrecords.Shared;
using Utility;
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

        /// <summary>
        /// Creature inherits sounds from other creature
        /// </summary>
        public CSCR CSCR { get; set; }

        /// <summary>
        /// Creature sounds
        /// CSDT - Type
        /// CSDI - Chance
        /// CSDC - SoundFormId
        /// </summary>
        public List<(CSDT CSDT, CSDI CSDI, CSDC CSDC)> SNDS { get; set; }

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

        protected override void BuildSubrecords()
        {
            var readerData = new ByteReader();
            SNDS = new List<(CSDT CSDT, CSDI CSDI, CSDC CSDC)>();
            while (Data.Length != readerData.offset)
            {
                var subrecordName = GetSubrecordName(readerData);
                var subrecordSize = GetSubrecordSize(readerData);

                if (subrecordName.Equals("CSDT"))
                {
                    SNDS.Add((new CSDT(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)), null, null));
                    continue;
                }
                if (subrecordName.Equals("CSDI"))
                {
                    int index = SNDS.Count - 1;
                    SNDS[index] = (SNDS[index].CSDT, new CSDI(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)), SNDS[index].CSDC);
                    continue;
                }
                if (subrecordName.Equals("CSDC"))
                {
                    int index = SNDS.Count - 1;
                    SNDS[index] = (SNDS[index].CSDT, SNDS[index].CSDI, new CSDC(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)));
                    continue;
                }

                try
                {
                    ReadSubrecords(readerData, subrecordName, subrecordSize);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building {this.GetType().ToString()} ar subrecord {subrecordName} eighter not implemented or borked {e}");
                    break;
                }
            }
        }
    }
}