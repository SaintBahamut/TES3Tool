using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    /// <summary>
    /// NPC flags
    /// </summary>
    public class FLAG : Subrecord
    {
        /// <summary>
        /// NPC Flags (4 bytes, bit field)
        /// 0x0001 = Female
		/// 0x0002 = Essential
		/// 0x0004 = Respawn
		/// 0x0008 = None?
		/// 0x0010 = Autocalc		
		/// 0x0400 = Blood Skel
		/// 0x0800 = Blood Metal
        /// </summary>
        public HashSet<NPCFlag> Flags { get; set; }

        public FLAG()
        {
            Flags = new HashSet<NPCFlag>();
        }

        public FLAG(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadFlagBytes<NPCFlag>(base.Data);
        }
    }
}
