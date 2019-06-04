using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.SPEL;
using Utility;
using static Utility.Common;

namespace TES3Lib.Records
{
    [DebuggerDisplay("{NAME.EditorId}")]
    public class SPEL : Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public SPDT SPDT { get; set; }

        public List<ENAM> ENAM { get; set; }

        public SPEL()
        {
        }

        public SPEL(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var reader = new ByteReader();

            ENAM = new List<ENAM>();

            while (Data.Length != reader.offset)
            {
                var subrecordName = GetRecordName(reader);
                var subrecordSize = GetRecordSize(reader);

                try
                {
                    if (subrecordName.Equals("ENAM"))
                    {
                        ENAM.Add(new ENAM(reader.ReadBytes<byte[]>(Data, subrecordSize)));
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

                if (property.Name == "ENAM")
                {
                    List<byte> ranks = new List<byte>();
                    foreach (var rnam in ENAM)
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

        public override string GetEditorId()
        {
            return !IsNull(NAME) ? NAME.EditorId : null;
        }
    }
}
