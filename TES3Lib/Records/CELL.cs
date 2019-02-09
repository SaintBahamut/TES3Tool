using System;
using System.Collections.Generic;
using TES3Lib.Structures.Base;
using TES3Lib.Subrecords.CELL;
using Utility;

namespace TES3Lib.Records
{
    public class CELL : Record
    {
        public NAME NAME { get; set; }

        public DATA DATA { get; set; }

        public RGNN RGNN { get; set; }

        public NAM0 NAM0 { get; set; }

        //Exterior only
        public NAM5 NAM5 { get; set; }

        //Interior only
        public WHGT WHGT { get; set; }
        public AMBI AMBI { get; set; }

        private List<REFR> _REFR = new List<REFR>();

        public List<REFR> REFR
        {
            get { return _REFR; }
            set { _REFR = value; }
        }

        public CELL(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
   
            var readerData = new ByteReader();
            while (Data.Length != readerData.offset)
            {
                try
                {
                    var subrecordName = GetRecordName(readerData);
                    var subrecordSize = GetRecordSize(readerData);

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
                    Console.WriteLine($"error in building CELL record something is borkeeeed {e}");
                    break;
                }
            }



        }


    }
}
