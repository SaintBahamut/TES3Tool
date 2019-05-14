using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    public class XESP : Subrecord
    {
        public string ParentFormId { get; set; }

        public uint Flags { get; set; }

        public XESP(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ParentFormId = reader.ReadFormId(base.Data);
            Flags = reader.ReadBytes<uint>(base.Data);
        }
    }
}
