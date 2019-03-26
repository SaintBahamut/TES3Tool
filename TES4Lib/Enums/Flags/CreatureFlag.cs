namespace TES4Lib.Enums.Flags
{
    public enum CreatureFlag : uint
    {
        Biped = 0x000001,
        Essential = 0x000002,
        WeaponAndShield = 0x000004,
        Respawn = 0x000008,
        Swims = 0x000010,
        Flies = 0x000020,
        Walks = 0x000040,
        PCLevelOffset = 0x000080,
        //Unused=0x000100,
        NoLowLevelProcessing = 0x000100,
        //Unused2=0x000400,
        NoBloodSpray = 0x000800,
        NoBloodDecal = 0x001000,
        //Unused3 = 0x002000,
        //Unused4 = 0x004000,
        NoHead = 0x008000,
        NoRightArm = 0x010000,
        NoLeftArm = 0x020000,
        NoCombatInWater = 0x040000,
        NoShadow = 0x080000,
        NoCorpseCheck = 0x100000,
    }
}
