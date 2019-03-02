using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CONT
{
    /// <summary>
    /// Close sound
    /// </summary>
    public class QNAM : Subrecord
    {
        /// <summary>
        /// Sound formId
        /// </summary>
        public string SoundFormId { get; set; }

        public QNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var baseFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            SoundFormId = BitConverter.ToString(baseFormIdBytes.Reverse().ToArray()).Replace("-", "");
        }
    }
}
