using System;
using System.Collections.Generic;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.ACHR
{
    /// <summary>
    /// Horse
    /// </summary>
    public class XHRS : Subrecord
    {
        public string ParentFormId { get; set; }

        public uint Flags { get; set; }

        public XHRS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ParentFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
            Flags = reader.ReadBytes<uint>(base.Data);
        }
    }
}
