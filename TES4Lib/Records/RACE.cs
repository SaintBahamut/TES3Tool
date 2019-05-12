using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Subrecords.Shared;
using TES4Lib.Subrecords.RACE;
using CNAM = TES4Lib.Subrecords.RACE.CNAM;
using System;
using Utility;
using System.Diagnostics;

namespace TES4Lib.Records
{
    /// <summary>
    /// Race record
    /// </summary>
    [DebuggerDisplay("{EDID.EditorId}")]
    public class RACE : Record
    {
        /// <summary>
        /// Race EditorId
        /// </summary>
        public EDID EDID { get; set; }

        /// <summary>
        /// Race display name
        /// </summary>
        public FULL FULL { get; set; }

        /// <summary>
        /// Race description
        /// </summary>
        public DESC DESC { get; set; }

        /// <summary>
        /// Races default spells
        /// </summary>
        public List<SPLO> SPLO { get; set; }

        /// <summary>
        /// Disposition towards other races
        /// </summary>
        public XNAM XNAM { get; set; }

        /// <summary>
        /// Various parameters of race
        /// </summary>
        public DATA DATA { get; set; }

        /// <summary>
        /// Voices used by race
        /// </summary>
        public VNAM VNAM { get; set; }

        /// <summary>
        /// Default Hair for race
        /// </summary>
        public DNAM DNAM { get; set; }

        /// <summary>
        /// Default Hair Color
        /// </summary>
        public CNAM CNAM { get; set; }

        /// <summary>
        /// FaceGen - Main clamp
        /// </summary>
        public PNAM PNAM { get; set; }

        /// <summary>
        /// FaceGen - Face clamp
        /// </summary>
        public UNAM UNAM { get; set; }

        /// <summary>
        /// Base Attributes
        /// </summary>
        public ATTR ATTR { get; set; }

        //TODO IMPLEMENT REST SOMEDAY

        public RACE(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        protected override void BuildSubrecords()
        {
            var readerData = new ByteReader();
            while (Data.Length != readerData.offset)
            {
                var subrecordName = GetSubrecordName(readerData);
                var subrecordSize = GetSubrecordSize(readerData);

                try
                {
                    ReadSubrecords(readerData, subrecordName, subrecordSize);
                }
                catch (Exception e)
                {
                    //Console.WriteLine($"error in building {this.GetType().ToString()} ar subrecord {subrecordName} eighter not implemented or borked {e}");
                    //just fail sillently
                    break;
                }
            }
        }
    }
}