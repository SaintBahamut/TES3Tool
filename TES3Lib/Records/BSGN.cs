using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.BSGN;
using TES3Lib.Subrecords.Shared;
using Utility;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Birthsign Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class BSGN : Record
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
        /// Birthsign graphic
        /// </summary>
        public TNAM TNAM { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public DESC DESC { get; set; }

        /// <summary>
        /// Powers
        /// </summary>
        public List<NPCS> NPCS { get; set; }

        public BSGN()
        {
        }

        public BSGN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
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
