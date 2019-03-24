using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// NPC AI data subrecord
    /// </summary>
    public class AIDT : Subrecord
    {
        public byte Hello { get; set; }

        public byte Unknown1 { get; set; }

        public byte Fight { get; set; }

        public byte Flee { get; set; }

        public byte Alarm { get; set; }

        public byte Unknown2 { get; set; }

        public byte Unknown3 { get; set; }

        public byte Unknown4 { get; set; }

        /// <summary>
        /// NPC Service flags
        /// 0x00001 = Weapon
		///	0x00002 = Armor
		///	0x00004 = Clothing
		///	0x00008 = Books
		///	0x00010 = Ingrediant
		///	0x00020 = Picks
		///	0x00040 = Probes
		///	0x00080 = Lights
		///	0x00100 = Apparatus
		///	0x00200 = Repair
		///	0x00400 = Misc
		///	0x00800 = Spells
		///	0x01000 = Magic Items
		///	0x02000 = Potions
		///	0x04000 = Training
		///	0x08000 = Spellmaking
		///	0x10000 = Enchanting
		///	0x20000 = Repair Item
        /// </summary>
        public HashSet<ServicesFlag> Flags { get; set; }

        public AIDT()
        {
        }

        public AIDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Hello = reader.ReadBytes<byte>(base.Data);
            Unknown1 = reader.ReadBytes<byte>(base.Data);
            Fight = reader.ReadBytes<byte>(base.Data);
            Flee = reader.ReadBytes<byte>(base.Data);
            Alarm = reader.ReadBytes<byte>(base.Data);
            Unknown2 = reader.ReadBytes<byte>(base.Data);
            Unknown3 = reader.ReadBytes<byte>(base.Data);
            Unknown4 = reader.ReadBytes<byte>(base.Data);
            Flags = reader.ReadFlagBytes<ServicesFlag>(base.Data);
        }
    }
}