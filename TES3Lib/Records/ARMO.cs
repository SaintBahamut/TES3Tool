using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.ARMO;
using System.Collections.Generic;
using Utility;
using System;
using System.Reflection;
using System.Linq;
using System.Text;

namespace TES3Lib.Records
{
    public class ARMO : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public AODT AODT { get; set; }

        public ITEX ITEX { get; set; }

        public List<BBSL> BBSL = new List<BBSL>();

        public SCRI SCRI { get; set; }

        public ENAM ENAM { get; set; }

        public ARMO(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var reader = new ByteReader();
            while (Data.Length != reader.offset)
            {
                try
                {
                    var subrecordName = GetRecordName(reader);
                    var subrecordSize = GetRecordSize(reader);

                    if (subrecordName.Equals("INDX"))
                    {
                        BBSL bbsl = new BBSL();
                        bbsl.INDX = new INDX(reader.ReadBytes<byte[]>(Data, subrecordSize));

                        if (Data.Length != reader.offset) break;

                        string nextSubrecord = GetRecordName(reader);
                        int nextSize = GetRecordSize(reader);

                        if (nextSubrecord.Equals("BNAM"))
                        {
                            bbsl.BNAM = new BNAM(reader.ReadBytes<byte[]>(Data, nextSize));
                        }

                        if (Data.Length != reader.offset) break;

                        nextSubrecord = GetRecordName(reader);
                        nextSize = GetRecordSize(reader);

                        if (nextSubrecord.Equals("CNAM"))
                        {
                            bbsl.CNAM = new CNAM(reader.ReadBytes<byte[]>(Data, nextSize));
                        }

                        BBSL.Add(bbsl);
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
                    Console.WriteLine($"error in building ARMO record something is borkeeeed {e}");
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
                if (property.Name == "BBSL")
                {
                    if (BBSL.Count() > 0)
                    {
                        List<byte> containerItems = new List<byte>();
                        foreach (var bbsl in BBSL)
                        {
                            containerItems.AddRange(bbsl.INDX.SerializeSubrecord());
                            if (bbsl.BNAM != null) containerItems.AddRange(bbsl.BNAM.SerializeSubrecord());
                            if (bbsl.CNAM != null) containerItems.AddRange(bbsl.CNAM.SerializeSubrecord());
                        }
                        data.AddRange(containerItems.ToArray());
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

    /// <summary>
    /// Body part slot
    /// </summary>
    public class BBSL
    {
        public INDX INDX { get; set; }
        public BNAM BNAM { get; set; }
        public CNAM CNAM { get; set; }
    }
}
