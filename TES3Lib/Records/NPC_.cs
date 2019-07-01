using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using TES3Lib.Subrecords.NPC_;
using TES3Lib.Subrecords.Shared;
using Utility;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// NPC Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class NPC_ : Record
    {
        #region subrecords

        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public FNAM FNAM { get; set; }

        /// <summary>
        /// Race name: required even if empty
        /// </summary>
        public RNAM RNAM { get; set; }

        /// <summary>
        /// Class name
        /// </summary>
        public CNAM CNAM { get; set; }

        /// <summary>
        /// Faction name: required even if empty
        /// </summary>
        public ANAM ANAM { get; set; }

        /// <summary>
        /// Head model: required even if empty
        /// </summary>
        public BNAM BNAM { get; set; }

        /// <summary>
        /// Hair model: required even if empty
        /// </summary>
        public KNAM KNAM { get; set; }

        public SCRI SCRI { get; set; }

        public NPDT NPDT { get; set; }

        public FLAG FLAG { get; set; }

        public List<NPCO> NPCO { get; set; }

        public List<NPCS> NPCS { get; set; }

        public AIDT AIDT { get; set; }

        public List<(DODT coordinates, DNAM cell)> TravelService { get; set; }

        /// <summary>
        /// AI Packages
        /// CNDT Subrecord only present in packages:
        /// Follow (AI_F), Escort (AI_E)
        /// </summary>
        public List<(IAIPackage AIPackage, CNDT CNDT)> AIPackages { get; set; }

        public XSCL XSCL { get; set; }
        #endregion

        public NPC_()
        {
            NPCO = new List<NPCO>();
            NPCS = new List<NPCS>();
            AIPackages = new List<(IAIPackage AIPackage, CNDT CNDT)>();
            TravelService = new List<(DODT coordinates, DNAM cell)>();
        }

        public NPC_(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var readerData = new ByteReader();

            NPCO = new List<NPCO>();
            NPCS = new List<NPCS>();
            TravelService = new List<(DODT coordinates, DNAM cell)>();
            AIPackages = new List<(IAIPackage AIPackage, CNDT CNDT)>();

            while (Data.Length != readerData.offset)
            {
                var subrecordName = GetRecordName(readerData);
                var subrecordSize = GetRecordSize(readerData);
                try
                {               
                    if (subrecordName.Equals("DODT"))
                    {
                        TravelService.Add((new DODT(readerData.ReadBytes<byte[]>(Data, subrecordSize)), null));
                        continue;
                    }

                    if (subrecordName.Equals("DNAM"))
                    {
                        TravelService[TravelService.Count - 1] = (TravelService[TravelService.Count - 1].coordinates, new DNAM(readerData.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }

                    if (subrecordName.Equals("AI_W") || subrecordName.Equals("AI_T") || subrecordName.Equals("AI_F") || subrecordName.Equals("AI_E") || subrecordName.Equals("AI_A"))
                    {
                        Type packageType = Type.GetType($"TES3Lib.Subrecords.Shared.{subrecordName}");
                        IAIPackage aiPackage = Activator.CreateInstance(packageType, new object[] { readerData.ReadBytes<byte[]>(Data, subrecordSize) }) as IAIPackage;

                        CNDT CNDT = null;
                        if (Data.Length != readerData.offset)
                        {
                            subrecordName = GetRecordName(readerData);
                            subrecordSize = GetRecordSize(readerData);
                            if (subrecordName.Equals("CNDT"))
                                CNDT = new CNDT(readerData.ReadBytes<byte[]>(Data, subrecordSize));
                        }

                        AIPackages.Add((aiPackage, CNDT));
                        continue;
                    }
                    
                    ReadSubrecords(readerData, subrecordName, subrecordSize);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building NPC_ subrecord {subrecordName} , something is borkeeeed {e}");
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
                if (property.Name.Equals("TravelService"))
                {
                    if (TravelService.Count() > 0)
                    {
                        List<byte> travelDest = new List<byte>();
                        foreach (var destination in TravelService)
                        {
                            travelDest.AddRange(destination.coordinates.SerializeSubrecord());

                            if (!IsNull(destination.cell))
                                travelDest.AddRange(destination.cell.SerializeSubrecord());

                        }
                        data.AddRange(travelDest.ToArray());
                    }
                    continue;
                }

                if (property.Name.Equals("AIPackages"))
                {
                    if (AIPackages.Count > 0)
                    {
                        List<byte> aiPackagesBytes = new List<byte>();
                        foreach (var aiPackage in AIPackages)
                        {
                            aiPackagesBytes.AddRange(aiPackage.AIPackage.SerializeSubrecord());

                            if (!IsNull(aiPackage.CNDT))
                                aiPackagesBytes.AddRange(aiPackage.CNDT.SerializeSubrecord());

                        }
                        data.AddRange(aiPackagesBytes.ToArray());
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
                if (IsNull(subrecord)) continue;
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
