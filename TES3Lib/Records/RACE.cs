using System;
using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.RACE;
using TES3Lib.Subrecords.Shared;
using Utility;

namespace TES3Lib.Records
{
    public class RACE: Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public RADT RADT { get; set; }

        public List<NPCS> NPCS = new List<NPCS>();

        public DESC DESC { get; set; }

        public RACE()
        {
        }

        public RACE(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var reader = new ByteReader();
            while (Data.Length != reader.offset)
            {
                var subrecordName = GetRecordName(reader);
                var subrecordSize = GetRecordSize(reader);
                try
                {
                    if (subrecordName.Equals("NPCS"))
                    {
                        NPCS.Add(new NPCS(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }

                    var subrecordProp = this.GetType().GetProperty(subrecordName);
                    var subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { reader.ReadBytes<byte[]>(Data, subrecordSize) });
                    subrecordProp.SetValue(this, subrecord);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building CREA subrecord {subrecordName} , something is borkeeeed {e}");
                    break;
                }
            }
        }
    }
}
