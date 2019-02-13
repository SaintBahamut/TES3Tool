using System;
using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class XOWN : Subrecord
    {
        public string OwnerFormId { get; set; }

        public XOWN(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var ownerFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            OwnerFormId = BitConverter.ToString(ownerFormIdBytes).Replace("-", "");

        }
    }
}