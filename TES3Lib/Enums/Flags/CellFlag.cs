namespace TES3Lib.Enums.Flags
{
    public enum CellFlag : int
    {
        IsInteriorCell = 0x01,
        HasWater = 0x02,
        IllegalToSleep = 0x04,
        BehaveLikeExterior = 0x80, //Behave like exterior (Tribunal)
    }
}
