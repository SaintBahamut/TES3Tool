using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.FACT;
using TES3Lib.Subrecords.Shared;
using Utility;

namespace TES3Lib.Records
{
    public class FACT : Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public List<RNAM> RNAM = new List<RNAM>();

        public FADT FADT { get; set; }

        public List<(ANAM name, INTV value)> FactionsAttitudes = new List<(ANAM name, INTV value)>();

        public FACT(byte[] rawData) : base(rawData)
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
                    if (subrecordName.Equals("DODT"))
                    {
                        FactionsAttitudes.Add((new ANAM(reader.ReadBytes<byte[]>(Data, subrecordSize)), null));
                        continue;
                    }

                    if (subrecordName.Equals("DNAM"))
                    {
                        FactionsAttitudes[FactionsAttitudes.Count - 1] = (FactionsAttitudes[FactionsAttitudes.Count - 1].name, new INTV(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }

                    PropertyInfo subrecordProp = this.GetType().GetProperty(subrecordName);
                    object subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { reader.ReadBytes<byte[]>(Data, subrecordSize) });              

                    if (subrecordProp is IList)
                    {
                        var subRecordList = (List<Subrecord>)subrecordProp.GetValue(this);
                        subRecordList.Add((Subrecord)subrecord);
                    }
                    else
                        subrecordProp.SetValue(this, subrecord);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building {this.GetType().Name} subrecord {subrecordName} , something is borkeeeed {e}");
                    break;
                }
            }
        }

        public override byte[] SerializeRecord()
        {
            var properties = this.GetType()
                .GetProperties(System.Reflection.BindingFlags.Public |
                               System.Reflection.BindingFlags.Instance |
                               System.Reflection.BindingFlags.DeclaredOnly).OrderBy(x => x.MetadataToken).ToList();

            List<byte> data = new List<byte>();
            foreach (PropertyInfo property in properties)
            {

                if (property.Name == "RNAM")
                {
                    List<byte> ranks = new List<byte>();
                    foreach (var rnam in RNAM)
                    {
                        ranks.AddRange(rnam.SerializeSubrecord());
                    }
                    data.AddRange(ranks.ToArray());
                    continue;
                }

                if (property.Name == "TravelService")
                {
                    if (FactionsAttitudes.Count() > 0)
                    {
                        List<byte> facDisp = new List<byte>();
                        foreach (var attitude in FactionsAttitudes)
                        {
                            facDisp.AddRange(attitude.name.SerializeSubrecord());
                            facDisp.AddRange(attitude.value.SerializeSubrecord());

                        }
                        data.AddRange(facDisp.ToArray());
                        continue;
                    }
                }

                var subrecord = (Subrecord)property.GetValue(this);
                if (subrecord == null) continue;

                data.AddRange(subrecord.SerializeSubrecord());
            }

            return Encoding.ASCII.GetBytes(this.GetType().Name)
                .Concat(BitConverter.GetBytes(data.Count()))
                .Concat(BitConverter.GetBytes(Header))
                .Concat(BitConverter.GetBytes(Flags))
                .Concat(data).ToArray();
        }
    }
}
