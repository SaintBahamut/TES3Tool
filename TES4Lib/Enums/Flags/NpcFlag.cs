namespace TES4Lib.Enums.Flags
{
    public enum NPCFlag : int
    {
        Female = 0x000001,
        Essential = 0x000002,
        Respawn = 0x000008,
        AutoCalcStats = 0x000010,
        PCLevelOffset = 0x000080,
        NoLowLevelProcessing = 0x000200,
        NoRumors = 0x002000,
        Summonable = 0x004000,
        NoPersuasion = 0x008000,
        CanCorpseCheck = 0x100000,
    }
}
