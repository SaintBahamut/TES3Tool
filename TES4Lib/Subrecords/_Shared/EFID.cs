using System;
using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    public class EFID : Subrecord
    {
        public string MagicEffectCode { get; set; }

        public MagicEffect MagicEffect { get; set; }

        public EFID(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MagicEffectCode = reader.ReadBytes<string>(base.Data, 4);
            MagicEffect = (MagicEffect)(int)Enum.Parse(typeof(TES4Lib.Enums.MagicEffectCode), MagicEffectCode);
        }
    }
}