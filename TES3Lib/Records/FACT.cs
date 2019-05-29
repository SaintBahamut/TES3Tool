using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.FACT;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;
using Utility;
using TES3Lib.Enums.Flags;

namespace TES3Lib.Records
{
    public class FACT : Record
    {
        /// <summary>
        /// Faction id
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Faction name
        /// </summary>
        public FNAM FNAM { get; set; }

        public List<RNAM> RNAM { get; set; }

        public FADT FADT { get; set; }

        public List<(ANAM name, INTV value)> FactionsAttitudes = new List<(ANAM name, INTV value)>();

        public FACT()
        {
        }

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
                    if (subrecordName.Equals("ANAM"))
                    {
                        FactionsAttitudes.Add((new ANAM(reader.ReadBytes<byte[]>(Data, subrecordSize)), null));
                        continue;
                    }

                    if (subrecordName.Equals("INTV"))
                    {
                        FactionsAttitudes[FactionsAttitudes.Count - 1] = (FactionsAttitudes[FactionsAttitudes.Count - 1].name, new INTV(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }

                    //standard generic reader builder below, dont change
                    PropertyInfo subrecordProp = this.GetType().GetProperty(subrecordName);               
                    if (subrecordProp.PropertyType.IsGenericType)
                    {
                        var listType = subrecordProp.PropertyType.GetGenericArguments()[0];
                        if (IsNull(subrecordProp.GetValue(this)))
                        {
                            var IListRef = typeof(List<>);                   
                            Type[] IListParam = { listType };
                            object subRecordList = Activator.CreateInstance(IListRef.MakeGenericType(IListParam));
                            subrecordProp.SetValue(this, subRecordList);
                        }
                        object sub = Activator.CreateInstance(listType, new object[] { reader.ReadBytes<byte[]>(Data, subrecordSize) });

                        subrecordProp.GetValue(this).GetType().GetMethod("Add").Invoke(subrecordProp.GetValue(this), new[] { sub });
                        continue;
                    }
                    object subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { reader.ReadBytes<byte[]>(Data, subrecordSize) });
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

                if (property.Name == "FactionsAttitudes")
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

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
