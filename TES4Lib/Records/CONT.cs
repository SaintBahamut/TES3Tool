using System.Collections.Generic;
using TES4Lib.Subrecords.Shared;
using TES4Lib.Subrecords.CONT;
using TES4Lib.Base;
using Utility;
using System;

namespace TES4Lib.Records
{
    public class CONT : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }

        public DATA DATA { get; set; }

        public SNAM SNAM { get; set; }

        public QNAM QNAM { get; set; }

        public SCRI SCRI { get; set; }

        public List<CNTO> CNTO { get; set; }

        public CONT(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        protected override void BuildSubrecords()
        {
            CNTO = new List<CNTO>();

            var readerData = new ByteReader();
            while (Data.Length != readerData.offset)
            {
                try
                {
                    var subrecordName = GetSubrecordName(readerData);
                    var subrecordSize = GetSubrecordSize(readerData);
                    var subrecordProp = this.GetType().GetProperty(subrecordName);
                    var subrecordData = readerData.ReadBytes<byte[]>(Data, (int)subrecordSize);

                    if (subrecordName.Equals("CNTO"))
                    {
                        CNTO.Add(new CNTO(subrecordData));
                        continue;
                    }

                    var subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { subrecordData });
                    subrecordProp.SetValue(this, subrecord);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building {this.GetType().ToString()} eighter not implemented or borked {e}");
                    break;
                }
            }
        }
    }
}
