using System;
using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.RACE;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// TES3 Race record
    /// </summary>
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
        }

        public RACE(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
