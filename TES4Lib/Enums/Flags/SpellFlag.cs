namespace TES4Lib.Enums.Flags
{
    public enum SpellFlag : uint
    {
        ManualSpellCost = 0x00000001,
        PlayerStartSpell = 0x00000004,
        ImmuneToSilence = 0x0000000A,
        AreaEffectIgnoresLOS= 0x00000010,
        ScriptEffectAlwaysApplies= 0x00000020,
        DisallowSpellAbsorb = 0x00000040,
        DefaultUninitialized = 0xCDCDCD00,
    }
}
