﻿using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class BNAM : Subrecord
    {
        public string RankId { get; set; }

        public BNAM()
        {

        }

        public BNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            RankId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}