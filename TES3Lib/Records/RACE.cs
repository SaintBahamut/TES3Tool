using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.RACE;
using TES3Lib.Subrecords.Shared;
using Utility;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// TES3 Race record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class RACE: Record
    {
        /// <summary>
        /// Race EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Race Display Name
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Attributes and skill bonuses
        /// </summary>
        public RADT RADT { get; set; }

        /// <summary>
        /// Racial spells and powers
        /// </summary>
        public List<NPCS> NPCS { get; set; }

        /// <summary>
        /// Races description
        /// </summary>
        public DESC DESC { get; set; }

        public RACE()
        {
            NAME = new NAME();
            FNAM = new FNAM();
            DESC = new DESC();
            RADT = new RADT();
            NPCS = new List<NPCS>();
        }

        public RACE(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
