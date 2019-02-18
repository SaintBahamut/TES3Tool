﻿using TES3Lib.Base;

namespace TES3Lib.Records
{
    public class BOOK : Record
    {
        public BOOK(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}
