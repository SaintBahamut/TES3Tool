using TES4Lib.Base;
using TES4Lib.Enums;
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
        public byte Flags { get; set; }

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
            Flags = reader.ReadBytes<byte>(base.Data);
            Skill = (Skill)reader.ReadBytes<byte>(base.Data);
            Value = reader.ReadBytes<int>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
        }

        #region flag handlers
        public bool IsScroll() => CheckIfByteSet(Flags, 0x0001);
        public bool IsCantBeTaken() => CheckIfByteSet(Flags, 0x0002);
        #endregion
    }
}
