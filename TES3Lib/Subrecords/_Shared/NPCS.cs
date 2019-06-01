using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Spell/Ability/Power Subrecord
    /// </summary>
    [DebuggerDisplay("{SpellId}")]
    public class NPCS : Subrecord
    {
        /// <summary>
        /// Spell/Ability/PowerId (32 character)
        /// </summary>
        public string SpellId { get; set; }

        public NPCS()
        {
        }

        public NPCS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SpellId = reader.ReadBytes<string>(base.Data, 32);
        }
    }
}
