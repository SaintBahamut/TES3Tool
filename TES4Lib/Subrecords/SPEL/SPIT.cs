using TES4Lib.Base;
using Utility;
using static Utility.Common;

namespace TES4Lib.Subrecords.SPEL
{
    /// <summary>
    /// Spell data
    /// </summary>
    public class SPIT : Subrecord
    {
        public SpellType Type { get; set; }

        public int SpellCost { get; set; }

        public SpellLevel SpellLevel { get; set; }

        public int Flags { get; set; }

        public SPIT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = (SpellType)reader.ReadBytes<int>(base.Data);
            SpellCost = reader.ReadBytes<int>(base.Data);
            SpellLevel = (SpellLevel)reader.ReadBytes<int>(base.Data);
            Flags = reader.ReadBytes<int>(base.Data);
        }
    }
}
