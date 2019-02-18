﻿using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class REGN : Record
    {
        public REGN(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
