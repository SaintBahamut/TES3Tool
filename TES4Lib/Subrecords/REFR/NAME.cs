using System;
using TES4Lib.Structures.Base;
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
            BaseFormId = BitConverter.ToString(baseFormIdBytes).Replace("-", "");

        }
    }
}
