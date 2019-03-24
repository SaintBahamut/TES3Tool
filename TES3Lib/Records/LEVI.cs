using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using TES3Lib.Subrecords.LEVI;
using TES3Lib.Subrecords.Shared;
using Utility;

namespace TES3Lib.Records
{
    /// <summary>
    /// Leveled items
    /// </summary>
    public class LEVI : Record
    {
        public NAME NAME { get; set; }

        public DATA DATA { get; set; }

        public NNAM NNAM { get; set; }

        public INDX INDX { get; set; }

        List<(INAM INAM, INTV INTV)> ITEM { get; set; }

        public LEVI()
        {
            ITEM = new List<(INAM INAM, INTV INTV)>();
        }

        public LEVI(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var reader = new ByteReader();
            ITEM = new List<(INAM INAM, INTV INTV)>();
            while (Data.Length != reader.offset)
            {
                var subrecordName = GetRecordName(reader);
                var subrecordSize = GetRecordSize(reader);
                try
                {

                    if (subrecordName.Equals("INAM"))
                    {
                        ITEM.Add((new INAM(reader.ReadBytes<byte[]>(Data, subrecordSize)), null));
                        continue;
                    }

                    if (subrecordName.Equals("INTV"))
                    {
                        int index = ITEM.Count - 1;
                        ITEM[index] = (ITEM[index].INAM, new INTV(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }

                    var subrecordProp = this.GetType().GetProperty(subrecordName);
                    var subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { reader.ReadBytes<byte[]>(Data, subrecordSize) });
                    subrecordProp.SetValue(this, subrecord);

                }
                catch (Exception e)
                {
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
                if (property.Name == "ITEM")
                {
                    if (ITEM.Count() > 0)
                    {
                        List<byte> containerItems = new List<byte>();
                        foreach (var bpsl in ITEM)
                        {
                            containerItems.AddRange(bpsl.INAM.SerializeSubrecord());
                            containerItems.AddRange(bpsl.INTV.SerializeSubrecord());

                        }
                        data.AddRange(containerItems.ToArray());
                    }
                    continue;
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
    }
}
