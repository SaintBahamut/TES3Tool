using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using TES3Lib.Subrecords.LEVC;
using TES3Lib.Subrecords.Shared;
using Utility;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Leveled creatures Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class LEVC : Record
    {
        /// <summary>
        /// EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Flags
        /// </summary>
        public DATA DATA { get; set; }

        /// <summary>
        /// Chance
        /// </summary>
        public NNAM NNAM { get; set; }

        /// <summary>
        /// Numer of entries on CRIT List
        /// </summary>
        public INDX INDX { get; set; }

        /// <summary>
        /// List of actors
        /// CNAM - Creature/NPC EditorId
        /// INTV - PC level for previous CNAM
        /// </summary>
        public List<(CNAM CNAM, INTV INTV)> CRIT { get; set; }

        public LEVC()
        {
            CRIT = new List<(CNAM CNAM, INTV INTV)>();
        }

        public LEVC(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var reader = new ByteReader();
            CRIT = new List<(CNAM INAM, INTV INTV)>();
            while (Data.Length != reader.offset)
            {
                var subrecordName = GetRecordName(reader);
                var subrecordSize = GetRecordSize(reader);
                try
                {

                    if (subrecordName.Equals("CNAM"))
                    {
                        CRIT.Add((new CNAM(reader.ReadBytes<byte[]>(Data, subrecordSize)), null));
                        continue;
                    }

                    if (subrecordName.Equals("INTV"))
                    {
                        int index = CRIT.Count - 1;
                        CRIT[index] = (CRIT[index].CNAM, new INTV(reader.ReadBytes<byte[]>(Data, subrecordSize)));
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
                if (property.Name == "CRIT")
                {
                    if (CRIT.Count() > 0)
                    {
                        List<byte> containerItems = new List<byte>();
                        foreach (var crit in CRIT)
                        {
                            containerItems.AddRange(crit.CNAM.SerializeSubrecord());
                            containerItems.AddRange(crit.INTV.SerializeSubrecord());

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
