using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.DOOR
{
    /// <summary>
    /// Close sound
    /// </summary>
    public class ANAM : Subrecord
    {
        /// <summary>
        /// Sound formId
        /// </summary>
        public string SoundFormId { get; set; }

        public ANAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var baseFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            SoundFormId = BitConverter.ToString(baseFormIdBytes.Reverse().ToArray()).Replace("-", "");
        }
    }
}
