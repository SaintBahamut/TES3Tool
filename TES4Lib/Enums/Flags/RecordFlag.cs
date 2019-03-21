namespace TES4Lib.Enums.Flags
{
    public enum RecordFlag : uint
    {
        ESM = 0x00000001,
        Unknown1 = 0x00000002,
        Unknown2 = 0x00000004,
        Unknown3 = 0x00000008,
        Unknown4 = 0x000000010,
        Deleted = 0x00000020,
        BorderRegion_ActorValue = 0x00000040,
        TurnOffFire_ActorValue = 0x00000080,
        Unnown5 = 0x00000100,
        CastsShadows = 0x00000200,
        Questitem_PersistentReference_ShowInMenu = 0x00000400,
        InitiallyDisabled = 0x00000800,
        Ignored = 0x00001000,
        Unknown5 = 0x00002000,
        Unknown6 = 0x00004000,
        VisibleWhenDistant = 0x00008000,
        Unknown7 = 0x00010000,
        Dangerous_OffLimits = 0x00020000,
        Compressed = 0x00040000,
        CantWait = 0x00080000,
    }
}
