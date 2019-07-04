using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.FACT;
using TES3Lib.Subrecords.Shared;
using Utility;
using TES3Lib.Enums.Flags;
using System.Diagnostics;

namespace TES3Lib.Records
{
    /// <summary>
    /// Faction Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class FACT : Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// List of ranks
        /// </summary>
        public List<RNAM> RNAM { get; set; }

        /// <summary>
        /// Faction attributes and skills
        /// </summary>
        public FADT FADT { get; set; }

        /// <summary>
        /// Faction Attitudes towards other factions
        /// ANAM - EditorId
        /// INTV - Disposition modifier
        /// </summary>
        public List<(ANAM name, INTV value)> FactionsAttitudes { get; set; }

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

            RNAM = new List<RNAM>();
            FactionsAttitudes = new List<(ANAM name, INTV value)>();

            while (Data.Length != reader.offset)
            {
                var subrecordName = GetRecordName(reader);
                var subrecordSize = GetRecordSize(reader);
          
                try
                {
                    if (subrecordName.Equals("RNAM"))
                    {
                        RNAM.Add(new RNAM(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }
                    else if (subrecordName.Equals("ANAM"))
                    {
                        FactionsAttitudes.Add((new ANAM(reader.ReadBytes<byte[]>(Data, subrecordSize)), null));
                        continue;
                    }
                    else if (subrecordName.Equals("INTV"))
                    {
                        FactionsAttitudes[FactionsAttitudes.Count - 1] = (FactionsAttitudes[FactionsAttitudes.Count - 1].name, new INTV(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }

                    ReadSubrecords(reader, subrecordName, subrecordSize);
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
