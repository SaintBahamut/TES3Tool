using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Enchantment reference (formId)
    /// </summary>
    public class ENAM : Subrecord
    {
        /// <summary>
        /// Enchantment formId
        /// </summary>
        public string EnchantmentFormId { get; set; }

        public ENAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var baseFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            EnchantmentFormId = BitConverter.ToString(baseFormIdBytes.Reverse().ToArray()).Replace("-", "");
        }
    }
}

