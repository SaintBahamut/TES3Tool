using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.LVLI
{
    /// <summary>
    /// Leveled item data
    /// For new ESPs the Unknown1/Unknown2 seem to be the same value (0x02D6).
    /// The values vary in Oblivion.esm and are not always the same.
    /// </summary>
    public class LVLO : Subrecord
    {
        /// <summary>
        /// Item level
        /// </summary>
        public short Level { get; set; }

        /// <summary>
        /// Unknown 2bytes
        /// </summary>
        public short Unused1 { get; set; }

        /// <summary>
        /// Leveled item formId
        /// </summary>
        public string ItemFormId { get; set; }

        /// <summary>
        /// Item quantity
        /// </summary>
        public short Count { get; set; }

        /// <summary>
        /// Unknown 2 bytes
        /// </summary>
        public short Unused2 { get; set; }

        public LVLO(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Level = reader.ReadBytes<short>(base.Data);
            Unused1 = reader.ReadBytes<short>(base.Data);
            ItemFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(RawData, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
            Count = reader.ReadBytes<short>(base.Data);
            Unused2 = reader.ReadBytes<short>(base.Data);
        }
    }
}
