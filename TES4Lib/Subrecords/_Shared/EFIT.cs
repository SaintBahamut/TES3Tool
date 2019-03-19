using System;
using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    public class EFIT : Subrecord
    {
        public string MagicEffectCode { get; set; }

        public MagicEffect MagicEffect { get; set; }

        public int Magnitude { get; set; }

        public int Area { get; set; }

        public int Duration { get; set; }

        public SpellTarget Type { get; set; }

        public ActorValue ActorValue { get; set; }

        public EFIT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MagicEffectCode = reader.ReadBytes<string>(base.Data, 4);
            MagicEffect = (MagicEffect)(int)Enum.Parse(typeof(TES4Lib.Enums.MagicEffectCode), MagicEffectCode);
            Magnitude = reader.ReadBytes<int>(base.Data);
            Area = reader.ReadBytes<int>(base.Data);
            Duration = reader.ReadBytes<int>(base.Data);
            Type = (SpellTarget)reader.ReadBytes<int>(base.Data);
            ActorValue = (ActorValue)reader.ReadBytes<int>(base.Data);
        }
    }
}