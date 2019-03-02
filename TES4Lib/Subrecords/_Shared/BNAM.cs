using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Loop sound
    /// </summary>
    public class BNAM : Subrecord
    {
        /// <summary>
        /// Sound formId
        /// </summary>
        public string SoundFormId { get; set; }

        public BNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var baseFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            SoundFormId = BitConverter.ToString(baseFormIdBytes.Reverse().ToArray()).Replace("-", "");
        }
    }
}
