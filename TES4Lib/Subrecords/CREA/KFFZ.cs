﻿using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    public class KFFZ : Subrecord
    {
        public KFFZ(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
        }
    }
}