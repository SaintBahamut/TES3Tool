using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums;
using TES4Lib.Enums.Flags;
using Utility;
using static Utility.Common;

namespace TES4Lib.Subrecords.BOOK
{
    /// <summary>
    /// Display name of record
    /// </summary>
    public class DATA : Subrecord
    {
        /// <summary>
        /// Book flags
        // 0x0001 = Scroll
        // 0x0002 = Can't be taken 
        /// </summary>
        public HashSet<BookFlag> Flags { get; set; }

        /// <summary>
        /// Which skill the book teaches.
        /// Set to 0xFF if no skill is taught.
        /// </summary>
        public Skill Skill { get; set; }

        /// <summary>
        /// Book value
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Book weight
        /// </summary>
        public float Weight { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadFlagBytes<BookFlag>(base.Data);
            Skill = reader.ReadBytes<Skill>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
        }
    }
}
