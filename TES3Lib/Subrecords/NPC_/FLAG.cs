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
        /// NPC Flags (4 bytes, bit field)
        /// 0x0001 = Female
		/// 0x0002 = Essential
		/// 0x0004 = Respawn
		/// 0x0008 = None?
		/// 0x0010 = Autocalc		
		/// 0x0400 = Blood Skel
		/// 0x0800 = Blood Metal
        /// </summary>

        public int Flags { get; set; }

        public FLAG()
        {
            Flags = 0;
        }

        public FLAG(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadBytes<int>(base.Data);
        }

        #region flag handlers
        public bool IsFemale() => 0 != (Flags & 0x0001);
        public bool IsEssential() => 0 != (Flags & 0x0002);
        public bool IsRespawn() => 0 != (Flags & 0x0004);
        public bool IsNone() => 0 != (Flags & 0x0008);
        public bool IsAutoCalcStats() => 0 != (Flags | 0x0010);
        public bool IsBloodSkel() => 0 != (Flags & 0x0400);
        public bool IsBloodMetal() => 0 != (Flags & 0x0800);

        //TODO: make idempotent
        public void SetFemale() => Flags = Flags | 0x0001;
        public void SetEssential() => Flags = Flags | 0x0002;
        public void SetRespawn() => Flags = Flags | 0x0004;
        public void SetNone() => Flags = Flags | 0x0008;
        public void SetAutoCalcStats() => Flags = Flags | 0x0010;
        public void SetBloodSkel() => Flags = Flags | 0x0400;
        public void SetBloodMetal() => Flags = Flags | 0x0800;
        #endregion
    }
}
