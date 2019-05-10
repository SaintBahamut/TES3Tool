using System;
using System.Collections.Generic;
using System.Reflection;
using TES4Lib.Base;
using TES4Lib.Subrecords.FACT;
using TES4Lib.Subrecords.Shared;
using Utility;
using CNAM = TES4Lib.Subrecords.FACT.CNAM;
using INAM = TES4Lib.Subrecords.FACT.INAM;

namespace TES4Lib.Records
{
    public class FACT : Record
    {
        /// <summary>
        /// Faction EditorId
        /// </summary>
        public EDID EDID { get; set; }

        /// <summary>
        /// Faction display name
        /// </summary>
        public FULL FULL { get; set; }

        /// <summary>
        /// Faction interation with other factions
        /// </summary>
        public List<XNAM> XNAM { get; set; }

        /// <summary>
        /// Faction flags
        /// </summary>
        public DATA DATA { get; set; }

        /// <summary>
        /// Crime gold multiplayer
        /// </summary>
        public CNAM CNAM { get; set; }

        /// <summary>
        /// Faction ranks
        /// </summary>
        public List<(RNAM RNAM, MNAM MNAM, FNAM FNAM, INAM INAM)> RNKS { get; set; }


        public FACT(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        protected override void BuildSubrecords()
        {
            var readerData = new ByteReader();
            RNKS = new List<(RNAM RNAM, MNAM MNAM, FNAM FNAM, INAM INAM)>();
            while (Data.Length != readerData.offset)
            {
                var subrecordName = GetSubrecordName(readerData);
                var subrecordSize = GetSubrecordSize(readerData);

                if (subrecordName.Equals("RNAM"))
                {
                    RNKS.Add((new RNAM(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)), null, null, null));
                    continue;
                }

                if (subrecordName.Equals("MNAM"))
                {
                    int index = RNKS.Count - 1;
                    RNKS[index] = (RNKS[index].RNAM, new MNAM(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)), RNKS[index].FNAM, RNKS[index].INAM);
                    continue;
                }

                if (subrecordName.Equals("FNAM"))
                {
                    int index = RNKS.Count - 1;
                    RNKS[index] = (RNKS[index].RNAM, RNKS[index].MNAM, new FNAM(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)), RNKS[index].INAM);
                    continue;
                }

                if (subrecordName.Equals("INAM"))
                {
                    int index = RNKS.Count - 1;
                    RNKS[index] = (RNKS[index].RNAM, RNKS[index].MNAM, RNKS[index].FNAM, new INAM(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)));
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

        private bool IsNull(object v)
        {
            throw new NotImplementedException();
        }
    }
}