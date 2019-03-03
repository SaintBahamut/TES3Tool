using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    /// <summary>
    /// NPC flags
    /// </summary>
    public class FLAG : Subrecord
    {
        /// <summary>
        /// Creature Flags (4 bytes, bit field)
        /// 0x0001 = Female
		/// 0x0002 = Essential
		/// 0x0004 = Respawn
		/// 0x0008 = None?
		/// 0x0010 = Autocalc		
		/// 0x0400 = Blood Skel
		/// 0x0800 = Blood Metal
        /// </summary>

        public int Flags { get; set; }

        public FLAG(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadBytes<int>(base.Data);
        }

        public bool IsFemale() => 0 == (Flags & 0x0001);
        public bool IsEssential() => 0 == (Flags & 0x0002);
        public bool IsRespawn() => 0 == (Flags & 0x0004);
        public bool IsNone() => 0 == (Flags & 0x0008);
        public bool IsAutoCalcStats() => 0 == (Flags | 0x0010);
        public bool IsBloodSkel() => 0 == (Flags & 0x0400);
        public bool IsBloodMetal() => 0 == (Flags & 0x0800);

        public void TagFemale() => Flags = Flags | 0x0001;
        public void TagEssential() => Flags = Flags | 0x0002;
        public void TagRespawn() => Flags = Flags | 0x0004;
        public void TagNone() => Flags = Flags | 0x0008;
        public void TagAutoCalcStats() => Flags = Flags | 0x0010;
        public void TagBloodSkel() => Flags = Flags | 0x0400;
        public void TagBloodMetal() => Flags = Flags | 0x0800;
    }
}
