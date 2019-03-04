using TES3Lib.Base;
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
        public bool IsBiped() => 0 != (Flags & 0x0001);
        public bool IsRespawnable() => 0 != (Flags & 0x0002);
        public bool IsWeaponAndShield() => 0 != (Flags & 0x0004);
        public bool IsNone() => 0 != (Flags & 0x0008);
        public bool IsSwimming() => 0 != (Flags | 0x0010);
        public bool IsFlying() => 0 != (Flags & 0x0020);
        public bool IsWalk() => 0 != (Flags & 0x0040);
        public bool IsDefaultFlag() => 0 != (Flags & 0x0048);
        public bool IsEssential() => 0 != (Flags | 0x0080);
        public bool IsSkeletonBlood() => 0 != (Flags & 0x0400);
        public bool IsMetalBlood() => 0 != (Flags & 0x0800);

        //TODO: make idempotent
        public void SetBiped() => Flags = Flags | 0x0001;
        public void SetRespawnable() => Flags = Flags | 0x0002;
        public void SetWeaponAndShield() => Flags = Flags | 0x0004;
        public void SetNone() => Flags = Flags | 0x0008;
        public void SetSwimming() => Flags = Flags | 0x0010;
        public void SetFlying() => Flags = Flags | 0x0020;
        public void SetWalk() => Flags = Flags | 0x0040;
        public void SetDefaultFlag() => Flags = Flags | 0x0048;
        public void SetEssential() => Flags = Flags | 0x0080;
        public void SetSkeletonBlood() => Flags = Flags | 0x0400;
        public void SetMetalBlood() => Flags = Flags | 0x0800;
        #endregion
    }
}
