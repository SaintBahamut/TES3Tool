﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using TES3Lib.Subrecords.CELL;
using Utility;

namespace TES3Lib.Records
{
    public class CELL : Record
    {
        public NAME NAME { get; set; }

        public DATA DATA { get; set; }

        public RGNN RGNN { get; set; }

        public INTV INTV { get; set; }

        public NAM0 NAM0 { get; set; }

        //Exterior only
        public NAM5 NAM5 { get; set; }

        //Interior only
        public WHGT WHGT { get; set; }

        public AMBI AMBI { get; set; }

        public List<REFR> REFR { get; set; }

        public CELL()
        {
            REFR = new List<REFR>();
        }

        public CELL(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var readerData = new ByteReader();
            REFR = new List<REFR>();
            while (Data.Length != readerData.offset)
            {
                var subrecordName = GetRecordName(readerData);
                var subrecordSize = GetRecordSize(readerData);
                try
                {
                    if (subrecordName.Equals("FRMR"))
                    {
                        var refrListType = this.GetType().GetProperty("REFR");
                        var reflist = (List<REFR>)refrListType.GetValue(this);
                        reflist.Add(new REFR(Data, readerData));
                    }
                    else
                    {
                        var subrecordProp = this.GetType().GetProperty(subrecordName);
                        var subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { readerData.ReadBytes<byte[]>(Data, subrecordSize) });
                        subrecordProp.SetValue(this, subrecord);
                    }
                }
                catch (Exception e)
                {
                    if (NAME != null)
                    {
                        Console.WriteLine(NAME.CellName);
                    }
                    Console.WriteLine($"error in building {this.GetType().ToString()} on {subrecordName} eighter not implemented or borked {e}");
                    break;
                }
            }
        }

        public override byte[] SerializeRecord()
        {
            var properties = this.GetType()
                .GetProperties(BindingFlags.Public |
                               BindingFlags.Instance |
                               BindingFlags.DeclaredOnly).OrderBy(x => x.MetadataToken).ToList();
    
            List<byte> data = new List<byte>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "REFR") continue;
                var subrecord = (Subrecord)property.GetValue(this);
                if (subrecord == null) continue;

                data.AddRange(subrecord.SerializeSubrecord());
            }

            if(REFR.Count() > 0)
            {
                List<byte> cellReferences = new List<byte>();
                foreach (var refr in REFR)
                {
                    cellReferences.AddRange(refr.SerializeRecord());
                }
                data.AddRange(cellReferences.ToArray());
            }

            uint flagSerialized = 0;
            foreach (RecordFlag flagElement in Flags)
            {
                flagSerialized = flagSerialized | (uint)flagElement;
            }

            return Encoding.ASCII.GetBytes(this.GetType().Name)
                .Concat(BitConverter.GetBytes(data.Count()))
                .Concat(BitConverter.GetBytes(Header))
                .Concat(BitConverter.GetBytes(flagSerialized))
                .Concat(data).ToArray();
        }

    }
}
