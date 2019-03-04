using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.CREA;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using System.Text;
using Utility;

namespace TES3Lib.Records
{
    public class CREA : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        public NPDT NPDT { get; set; }

        public FLAG FLAG { get; set; }

        public SCRI SCRI { get; set; }

        public List<NPCO> NPCO = new List<NPCO>();

        public List<NPCS> NPCS = new List<NPCS>();

        public AIDT AIDT { get; set; }

        public CNAM CNAM { get; set; }

        public AI_W AI_W { get; set; }

        public AI_T AI_T { get; set; }

        public AI_F AI_F { get; set; }

        public AI_E AI_E { get; set; }

        public AI_A AI_A { get; set; }

        public List<(DODT coordinates, DNAM cell)> TravelService = new List<(DODT coordinates, DNAM cell)>();

        public DODT DODT { get; set; }

        public DNAM DNAM { get; set; }

        public XSCL XSCL { get; set; }

        public CREA(byte[] rawData) : base(rawData)
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
                        TravelService.Add((new DODT(reader.ReadBytes<byte[]>(Data, subrecordSize)), null));
                        continue;
                    }

                    if (subrecordName.Equals("DNAM"))
                    {
                        TravelService[TravelService.Count-1] = (TravelService[TravelService.Count - 1].coordinates, new DNAM(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }

                    if (subrecordName.Equals("NPCO"))
                    {
                        NPCO.Add(new NPCO(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }
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
                    if (property.Name == "NPCO") continue;
                    if (property.Name == "NPCS") continue;
                    if (property.Name == "TravelService") continue;

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

            if (NPCS.Count() > 0)
            {
                List<byte> containerSpells = new List<byte>();
                foreach (var npcs in NPCS)
                {
                    containerSpells.AddRange(npcs.SerializeSubrecord());
                }
                data.AddRange(containerSpells.ToArray());
            }

            if (TravelService.Count() > 0)
            {
                List<byte> travelDest = new List<byte>();
                foreach (var destination in TravelService)
                {
                    travelDest.AddRange(destination.coordinates.SerializeSubrecord());
                    if(destination.cell != null) travelDest.AddRange(destination.cell.SerializeSubrecord());

                }
                data.AddRange(travelDest.ToArray());
            }

            return Encoding.ASCII.GetBytes(this.GetType().Name)
                .Concat(BitConverter.GetBytes(data.Count()))
                .Concat(BitConverter.GetBytes(Header))
                .Concat(BitConverter.GetBytes(Flags))
                .Concat(data).ToArray();
        }
    }
}
