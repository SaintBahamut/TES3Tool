using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    public class XOWN : Subrecord
    {
        public string OwnerFormId { get; set; }

        public XOWN(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            OwnerFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
        }
    }
}
