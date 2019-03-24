using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using Utility;

namespace TES3Lib.Subrecords.CREA
{
    /// <summary>
    /// NPC flags
    /// </summary>
    public class FLAG : Subrecord
    {
        /// <summary>
        /// Creature Flags (4 bytes, bit field)
        /// 0x0001 = Biped
		/// 0x0002 = Respawn
		/// 0x0004 = Weapon and shield
		/// 0x0008 = None
		/// 0x0010 = Swims
		/// 0x0020 = Flies
		/// 0x0040 = Walks	
		/// 0x0048 = Default flags
		/// 0x0080 = Essential
		/// 0x0400 = Skeleton Blood
		/// 0x0800 = Metal Blood
        /// </summary>
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
