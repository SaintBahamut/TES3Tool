using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.CLOT;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;
using Utility;
using System.Diagnostics;

namespace TES3Lib.Records
{
    /// <summary>
    /// Clothing Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class CLOT : Record
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
        /// Display name
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Clothing properties
        /// </summary>
        public CTDT CTDT { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public ITEX ITEX { get; set; }

        /// <summary>
        /// Body parts used by cloth
        /// INDX - body part index
        /// BNAM - male mody part
        /// CNAM - female body part
        /// </summary>
        public List<(INDX INDX, BNAM BNAM, CNAM CNAM)> BPSL { get; set; }

        /// <summary>
        /// Enhancement
        /// </summary>
        public ENAM ENAM { get; set; }

        /// <summary>
        /// Script
        /// </summary>
        public SCRI SCRI { get; set; }

        public CLOT()
        {
            BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>();
        }

        public CLOT(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var reader = new ByteReader();
            BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>();
            while (Data.Length != reader.offset)
            {
                var subrecordName = GetRecordName(reader);
                var subrecordSize = GetRecordSize(reader);

                try
                {
                    if (subrecordName.Equals("INDX"))
                    {
                        BPSL.Add((new INDX(reader.ReadBytes<byte[]>(Data, subrecordSize)), null, null));
                        continue;
                    }

                    if (subrecordName.Equals("BNAM"))
                    {
                        int index = BPSL.Count - 1;
                        BPSL[index] = (BPSL[index].INDX, new BNAM(reader.ReadBytes<byte[]>(Data, subrecordSize)), BPSL[index].CNAM);
                        continue;
                    }

                    if (subrecordName.Equals("CNAM"))
                    {
                        int index = BPSL.Count - 1;
                        BPSL[index] = (BPSL[index].INDX, BPSL[index].BNAM, new CNAM(reader.ReadBytes<byte[]>(Data, subrecordSize)));
                        continue;
                    }

                    var subrecordProp = this.GetType().GetProperty(subrecordName);
                    var subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { reader.ReadBytes<byte[]>(Data, subrecordSize) });
                    subrecordProp.SetValue(this, subrecord);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building {this.GetType().ToString()} on {subrecordName} eighter not implemented or borked {e}");
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
                if (property.Name.Equals("BPSL"))
                {
                    if (BPSL.Count() > 0)
                    {
                        List<byte> containerItems = new List<byte>();
                        foreach (var bpsl in BPSL)
                        {
                            containerItems.AddRange(bpsl.INDX.SerializeSubrecord());
                            if (!IsNull(bpsl.BNAM)) containerItems.AddRange(bpsl.BNAM.SerializeSubrecord());
                            if (!IsNull(bpsl.CNAM)) containerItems.AddRange(bpsl.CNAM.SerializeSubrecord());
                        }
                        data.AddRange(containerItems.ToArray());
                    }
                    continue;
                }
                var subrecord = (Subrecord)property.GetValue(this);
                if (IsNull(subrecord)) continue;

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
