using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.CONT;
using System.Collections.Generic;
using Utility;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class CONT : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public CNDT CNDT { get; set; }

        public FLAG FLAG { get; set; }

        public SCRI SCRI { get; set; }

        public List<NPCO> NPCO { get; set; }

        public CONT()
        {

        }

        public CONT(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var reader = new ByteReader();

            NPCO = new List<NPCO>();
 
            while (Data.Length != reader.offset)
            {
                try
                {
                    var subrecordName = GetRecordName(reader);
                    var subrecordSize = GetRecordSize(reader);

                    if (subrecordName.Equals("NPCO"))
                    {
                        NPCO.Add(new NPCO(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                    }
                    else
                    {
                        var subrecordProp = this.GetType().GetProperty(subrecordName);
                        var subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { reader.ReadBytes<byte[]>(Data, subrecordSize) });
                        subrecordProp.SetValue(this, subrecord);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building CONT record something is borkeeeed {e}");
                    break;
                }
            }
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
