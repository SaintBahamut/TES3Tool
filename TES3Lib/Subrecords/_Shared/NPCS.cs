using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Spell/Ability/Power Subrecord
    /// </summary>
    public class NPCS : Subrecord
    {
        /// <summary>
        /// Spell/Ability/PowerId (32 character)
        /// </summary>
        public string SpellId { get; set; }

        public NPCS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
