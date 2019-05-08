using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.ARMO;
using System.Collections.Generic;
using Utility;
using System;
using System.Reflection;
using System.Linq;
using System.Text;
using static Utility.Common;

namespace TES3Lib.Records
{
    public class ARMO : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public AODT AODT { get; set; }

        public ITEX ITEX { get; set; }

        /// <summary>
        /// INDX - body part index
        /// BNAM - male mody part
        /// CNAM - female body part
        /// </summary>
        public List<(INDX INDX, BNAM BNAM, CNAM CNAM)> BPSL { get; set; }

        public SCRI SCRI { get; set; }

        public ENAM ENAM { get; set; }

        public ARMO()
        {
            BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>();
        }

        public ARMO(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var reader = new ByteReader();
            BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>();
            while (Data.Length != reader.offset)
            {
                var subrecordName = GetRecordName(reader);
                var subrecordSize = GetRecordSize(reader);
                try
                { 
                    if (subrecordName.Equals("INDX"))
                    {
                        BPSL.Add((new INDX(reader.ReadBytes<byte[]>(Data, subrecordSize)), null, null));
                        continue;
                    }

                    if (subrecordName.Equals("BNAM"))
                    {
                        int index = BPSL.Count - 1;
                        BPSL[index] = (BPSL[index].INDX, new BNAM(reader.ReadBytes<byte[]>(Data, subrecordSize)), BPSL[index].CNAM);
                        continue;
                    }

                    if (subrecordName.Equals("CNAM"))
                    {
                        int index = BPSL.Count - 1;
                        BPSL[index] = (BPSL[index].INDX, BPSL[index].BNAM, new CNAM(reader.ReadBytes<byte[]>(Data, subrecordSize)));
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
                if (property.Name.Equals("BPSL"))
                {
                    if (BPSL.Count() > 0)
                    {
                        List<byte> containerItems = new List<byte>();
                        foreach (var bpsl in BPSL)
                        {
                            containerItems.AddRange(bpsl.INDX.SerializeSubrecord());
                            if (!IsNull(bpsl.BNAM)) containerItems.AddRange(bpsl.BNAM.SerializeSubrecord());
                            if (!IsNull(bpsl.CNAM)) containerItems.AddRange(bpsl.CNAM.SerializeSubrecord());
                        }
                        data.AddRange(containerItems.ToArray());                   
                    }
                    continue;
                }
                var subrecord = (Subrecord)property.GetValue(this);
                if (IsNull(subrecord)) continue;

                data.AddRange(subrecord.SerializeSubrecord());
            }

            return Encoding.ASCII.GetBytes(this.GetType().Name)
                .Concat(BitConverter.GetBytes(data.Count()))
                .Concat(BitConverter.GetBytes(Header))
                .Concat(BitConverter.GetBytes(SerializeFlag()))
                .Concat(data).ToArray();
        }
    }
}
