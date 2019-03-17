using System;
using System.Collections.Generic;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.INGR
{
    public class EFIT : Subrecord
    {
        public string MagicEffect { get; set; }

        public int Magnitude { get; set; }

        public int Area { get; set; }

        public int Duration { get; set; }

        /// <summary>
        /// self, touch, target
        /// </summary>
        public int Type { get; set; }

        public int ActorValue { get; set; }

        public EFIT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MagicEffect = reader.ReadBytes<string>(base.Data, 4);
            Magnitude = reader.ReadBytes<int>(base.Data);
            Area = reader.ReadBytes<int>(base.Data);
            Duration = reader.ReadBytes<int>(base.Data);
            Type = reader.ReadBytes<int>(base.Data);
            ActorValue = reader.ReadBytes<int>(base.Data);
        }
    }
}




//wbInteger('Magic effect name', itU32, wbChar4),
//      wbInteger('Magnitude', itU32),
//      wbInteger('Area', itU32),
//      wbInteger('Duration', itU32),
//      wbInteger('Type', itU32, wbEnum(['Self', 'Touch', 'Target'])),
//wbInteger('Actor Value', itS32, wbActorValueEnum)