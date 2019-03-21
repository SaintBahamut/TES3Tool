namespace TES4Lib.Enums.Flags
{
    public enum LightFlag : uint
    {
        Dynamic = 0x00000001,
        CanBeCarried = 0x00000002,
        Negative = 0x00000004,
        Flicker = 0x00000008,
        Unused = 0x00000010,
        OffByDefault = 0x00000020,
        FlickerSlow = 0x00000040,
        Pulse = 0x00000080,
        PulseSlow = 0x00000100,
        SpotLight = 0x00000200,
        SpotShadow = 0x00000400,
    }
}
