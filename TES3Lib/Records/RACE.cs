using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.RACE;
using TES3Lib.Subrecords.Shared;
using Utility;

namespace TES3Lib.Records
{
    public class RACE: Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public RADT RADT { get; set; }

        public List<NPCS> NPCS = new List<NPCS>();

        public DESC DESC { get; set; }



        public RACE(byte[] rawData) : base(rawData)
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
                    if (subrecordName.Equals("NPCS"))
                    {
                        NPCS.Add(new NPCS(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }

                    var subrecordProp = this.GetType().GetProperty(subrecordName);
                    var subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { reader.ReadBytes<byte[]>(Data, subrecordSize) });
                    subrecordProp.SetValue(this, subrecord);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building CREA subrecord {subrecordName} , something is borkeeeed {e}");
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
                    if (property.Name == "NPCS") continue;
                    
                    var subrecord = (Subrecord)property.GetValue(this);
                    if (subrecord == null) continue;

                    data.AddRange(subrecord.SerializeSubrecord());
                }
                catch (Exception)
                {

                    throw;
                }
            }

            if (NPCS.Count() > 0)
            {
                List<byte> containerSpells = new List<byte>();
                foreach (var npcs in NPCS)
                {
                    containerSpells.AddRange(npcs.SerializeSubrecord());
                }
                data.AddRange(containerSpells.ToArray());
            }

            return Encoding.ASCII.GetBytes(this.GetType().Name)
                .Concat(BitConverter.GetBytes(data.Count()))
                .Concat(BitConverter.GetBytes(Header))
                .Concat(BitConverter.GetBytes(Flags))
                .Concat(data).ToArray();
        }
    }
}
