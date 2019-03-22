namespace TES4Lib.Enums.Flags
{
    public enum CellFlag : byte
    {
        IsInteriorCell = 0x01,
        HasWater = 0x02,
        InvertFastTravelBehavior = 0x04,
        ForceHideLand = 0x08, //Force hide land (exterior cell) / Oblivion interior (interior cell)
        Unknown = 0x10,
        PublicPlace = 0x20,
        HandChanged = 0x40,
        BehaveLikeExterior = 0x80,
    }
}
