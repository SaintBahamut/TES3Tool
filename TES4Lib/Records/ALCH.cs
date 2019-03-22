using System;
using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Subrecords.ALCH;
using TES4Lib.Subrecords.Shared;
using Utility;
using static Utility.Common;

namespace TES4Lib.Records
{
    public class ALCH : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }

        public ICON ICON { get; set; }

        public SCRI SCRI { get; set; }

        public DATA DATA { get; set; }

        public ENIT ENIT { get; set; }

        public List<(EFID EFID, EFIT EFIT, SCIT SCIT, SULL FULL)> EFFECT { get; set; }

        public ALCH(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        protected override void BuildSubrecords()
        {
            var readerData = new ByteReader();
            EFFECT = new List<(EFID, EFIT, SCIT, SULL)>();
            while (Data.Length != readerData.offset)
            {
                var subrecordName = GetRecordName(readerData);
                var subrecordSize = GetRecordSize(readerData);

                if (subrecordName.Equals("EFID"))
                {
                    EFFECT.Add((new EFID(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)), null, null, null));
                    continue;
                }

                if (subrecordName.Equals("EFIT"))
                {
                    int index = EFFECT.Count - 1;
                    EFFECT[index] = (EFFECT[index].EFID, new EFIT(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)), EFFECT[index].SCIT, EFFECT[index].FULL);
                    continue;
                }

                if (subrecordName.Equals("SCIT"))
                {
                    int index = EFFECT.Count - 1;
                    EFFECT[index] = (EFFECT[index].EFID, EFFECT[index].EFIT, new SCIT(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)), EFFECT[index].FULL);
                    continue;
                }

                if (subrecordName.Equals("FULL") && !IsNull(this.FULL))
                {
                    int index = EFFECT.Count - 1;
                    EFFECT[index] = (EFFECT[index].EFID, EFFECT[index].EFIT, EFFECT[index].SCIT, new SULL(readerData.ReadBytes<byte[]>(Data, (int)subrecordSize)));
                    continue;
                }

                try
                {
                    var subrecordProp = this.GetType().GetProperty(subrecordName);
                    var subrecordData = readerData.ReadBytes<byte[]>(Data, (int)subrecordSize);

                    var subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { subrecordData });
                    subrecordProp.SetValue(this, subrecord);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building {this.GetType().ToString()} ar subrecord {subrecordName} eighter not implemented or borked {e}");
                    break;
                }
            }
        }
    }
}