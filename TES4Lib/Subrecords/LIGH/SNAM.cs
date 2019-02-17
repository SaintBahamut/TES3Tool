using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.LIGH
{
    public class SNAM : Subrecord
    {
        public string SoundFormId { get; set; }

        public SNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var baseFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            SoundFormId = BitConverter.ToString(baseFormIdBytes.Reverse().ToArray()).Replace("-", "");

        }
    }
}
