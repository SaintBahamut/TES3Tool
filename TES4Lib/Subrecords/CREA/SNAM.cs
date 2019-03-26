using System;
using System.Collections.Generic;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature faction data
    /// </summary>
    public class SNAM : Subrecord
    {
        public string FactionFormId { get; set; }
        public byte Rank { get; set; }
        public byte[] Unused { get; set; }

        public SNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FactionFormId = reader.ReadFormId(base.Data);
            Rank = reader.ReadBytes<byte>(base.Data);
            Unused = reader.ReadBytes<byte[]>(base.Data, 3);
        }
    }
}
