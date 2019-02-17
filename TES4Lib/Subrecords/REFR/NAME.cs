using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class NAME : Subrecord
    {
        public string BaseFormId { get; set; }

        public NAME(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var baseFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            BaseFormId = BitConverter.ToString(baseFormIdBytes.Reverse().ToArray()).Replace("-", "");

        }
    }
}
