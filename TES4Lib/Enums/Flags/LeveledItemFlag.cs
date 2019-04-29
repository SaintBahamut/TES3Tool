namespace TES4Lib.Enums.Flags
{
    public enum LeveledItemFlag : byte
    {
        /// <summary>
        /// Calc from all levels <= PC level
        /// </summary>
        CalcFromAllLevels = 1,
        CalcForEachItem = 2,
        UseAllSpells = 4
    }
}
