using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using Utility;

namespace TES3Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature flags
    /// </summary>
    public class FLAG : Subrecord
    {
        public HashSet<CreatureFlag> Flags { get; set; }

        public FLAG()
        {
            Flags = new HashSet<CreatureFlag>();
        }

        public FLAG(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadFlagBytes<CreatureFlag>(base.Data);
        }
    }
}
