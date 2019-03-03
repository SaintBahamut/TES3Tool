using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.NPC_;
using TES3Lib.Subrecords.Shared;
using Utility;

namespace TES3Lib.Records
{
    public class NPC_ : Record
    {
        public NAME NAME { get; set; }
        public FNAM FNAM { get; set; }
        public MODL MODL { get; set; }
        public RNAM RNAM { get; set; }
        public ANAM ANAM { get; set; }
        public BNAM BNAM { get; set; }
        public CNAM CNAM { get; set; }
        public KNAM KNAM { get; set; }
        public NPDT NPDT { get; set; }
        public FLAG FLAG { get; set; }
        public List<NPCO> NPCO = new List<NPCO>();
        public NPCS NPCS { get; set; }
        public AIDT AIDT { get; set; }
        public AI_W AI_W { get; set; }
        public AI_T AI_T { get; set; }
        public AI_F AI_F { get; set; }
        public AI_E AI_E { get; set; }
        public CNDT CNDT { get; set; }
        public AI_A AI_A { get; set; }
        public DODT DODT { get; set; }
        public DNAM DNAM { get; set; }
        public XSCL XSCL { get; set; }

        public NPC_(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
            IsImplemented = false;
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
                    Console.WriteLine($"error in building NPC_ record something is borkeeeed {e}");
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
                try
                {
                    if (property.Name == "NPCO") continue;
                    var subrecord = (Subrecord)property.GetValue(this);
                    if (subrecord == null) continue;

                    data.AddRange(subrecord.SerializeSubrecord());
                }
                catch (Exception)
                {

                    throw;
                }
            }

            if (NPCO.Count() > 0)
            {
                List<byte> containerItems = new List<byte>();
                foreach (var npco in NPCO)
                {
                    containerItems.AddRange(npco.SerializeSubrecord());
                }
                data.AddRange(containerItems.ToArray());
            }

            return Encoding.ASCII.GetBytes(this.GetType().Name)
                .Concat(BitConverter.GetBytes(data.Count()))
                .Concat(BitConverter.GetBytes(Header))
                .Concat(BitConverter.GetBytes(Flags))
                .Concat(data).ToArray();
        }
    }
}
