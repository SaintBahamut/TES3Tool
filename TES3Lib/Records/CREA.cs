using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.CREA;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using System.Text;
using Utility;
using TES3Lib.Enums.Flags;
using static Utility.Common;
using System.Collections;
using System.Diagnostics;

namespace TES3Lib.Records
{
    /// <summary>
    /// Creature Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class CREA : Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Model
        /// </summary>
        public MODL MODL { get; set; }

        /// <summary>
        /// Sound set
        /// </summary>
        public CNAM CNAM { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Script
        /// </summary>
        public SCRI SCRI { get; set; }

        /// <summary>
        /// Stats
        /// </summary>
        public NPDT NPDT { get; set; }

        /// <summary>
        /// Flags
        /// </summary>
        public FLAG FLAG { get; set; }

        /// <summary>
        /// Scale
        /// </summary>
        public XSCL XSCL { get; set; }

        /// <summary>
        /// Items held
        /// </summary>
        public List<NPCO> NPCO { get; set; }

        /// <summary>
        /// Spells
        /// </summary>
        public List<NPCS> NPCS { get; set; }

        /// <summary>
        /// Creature behavior
        /// </summary>
        public AIDT AIDT { get; set; }

        /// <summary>
        /// Travel service destinations
        /// </summary>
        public List<(DODT coordinates, DNAM cell)> TravelService { get; set; }

        /// <summary>
        /// AI Wander
        /// </summary>
        public AI_W AI_W { get; set; }

        /// <summary>
        /// AI Travel
        /// </summary>
        public AI_T AI_T { get; set; }

        /// <summary>
        /// AI Follow
        /// </summary>
        public AI_F AI_F { get; set; }

        /// <summary>
        /// AI Escort
        /// </summary>
        public AI_E AI_E { get; set; }

        /// <summary>
        /// AI Activate
        /// </summary>
        public AI_A AI_A { get; set; }

        public CREA()
        {
        }

        public CREA(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var reader = new ByteReader();

            NPCO = new List<NPCO>();
            NPCS = new List<NPCS>();
            TravelService = new List<(DODT coordinates, DNAM cell)>();

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
                        TravelService[TravelService.Count - 1] = (TravelService[TravelService.Count - 1].coordinates, new DNAM(reader.ReadBytes<byte[]>(Data, subrecordSize)));
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
                    Console.WriteLine($"error in building {this.GetType().ToString()} on {subrecordName} either not implemented or borked {e}");
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
                if (property.Name == "TravelService")
                {
                    if (TravelService.Count() > 0)
                    {
                        List<byte> travelDest = new List<byte>();
                        foreach (var destination in TravelService)
                        {
                            travelDest.AddRange(destination.coordinates.SerializeSubrecord());
                            if (destination.cell != null) travelDest.AddRange(destination.cell.SerializeSubrecord());

                        }
                        data.AddRange(travelDest.ToArray());
                    }
                    continue;
                }

                if (property.PropertyType.IsGenericType)
                {
                    var subrecordList = property.GetValue(this) as IEnumerable;
                    if (IsNull(subrecordList)) continue;
                    foreach (var sub in subrecordList)
                    {
                        data.AddRange((sub as Subrecord).SerializeSubrecord());
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

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
