﻿using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class MODL : Subrecord
    {
        public MODL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}
