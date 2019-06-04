using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.RACE;
using TES3Lib.Subrecords.Shared;
using Utility;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// TES3 Race record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class RACE: Record
    {
        /// <summary>
        /// Race EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Race Display Name
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Attributes and skill bonuses
        /// </summary>
        public RADT RADT { get; set; }

        /// <summary>
        /// Racial spells and powers
        /// </summary>
        public List<NPCS> NPCS { get; set; }

        /// <summary>
        /// Races description
        /// </summary>
        public DESC DESC { get; set; }

        public RACE()
        {
        }

        public RACE(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var reader = new ByteReader();

            NPCS = new List<NPCS>();

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
                .GetProperties(BindingFlags.Public |
                               BindingFlags.Instance |
                               BindingFlags.DeclaredOnly).OrderBy(x => x.MetadataToken).ToList();

            List<byte> data = new List<byte>();
            foreach (PropertyInfo property in properties)
            {

                if (property.Name == "NPCS")
                {
                    List<byte> ranks = new List<byte>();
                    foreach (var rnam in NPCS)
                    {
                        ranks.AddRange(rnam.SerializeSubrecord());
                    }
                    data.AddRange(ranks.ToArray());
                    continue;
                }

                var subrecord = (Subrecord)property.GetValue(this);
                if (subrecord == null) continue;

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
