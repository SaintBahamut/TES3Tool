using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Base
{
    [DebuggerDisplay("{Type} {Label}")]
    public class Group
    {
        readonly public string Name;
        public int Size { get; set; }
        public dynamic Label { get; set; }
        public GroupLabel Type { get; set; }
        public int Stamp { get; set; }
        public byte[] Data { get; set; }
        private byte[] RawData { get; set; }


        private List<Record> records = new List<Record>();

        public List<Record> Records
        {
            get { return records; }
        }

        private List<Group> groups = new List<Group>();

        public List<Group> Groups
        {
            get { return groups; }
        }

        public Group(byte[] rawData)
        {
            RawData = rawData;
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(RawData, 4);
            Size = reader.ReadBytes<int>(RawData);
            var labelRaw = reader.ReadBytes<byte[]>(RawData, 4);
            Type = reader.ReadBytes<GroupLabel>(RawData);
            Label = GenerateLabel(Type, labelRaw);
            Stamp = reader.ReadBytes<int>(RawData);
            Data = reader.ReadBytes<byte[]>(RawData, RawData.Length - 20);

            if (this.Label is string && this.Label == "WRLD")
            {
                BuildWRLDGroup();
            }
            else
                BuildRecords();
        }

        private dynamic GenerateLabel(GroupLabel type, byte[] rawLabel)
        {
            switch (type)
            {
                case GroupLabel.TopGroup:
                    return Encoding.ASCII.GetString(rawLabel);
                case GroupLabel.WorldChildren:
                case GroupLabel.CellChildren:
                case GroupLabel.TopicChildren:
                case GroupLabel.CellPersistentChildren:
                case GroupLabel.CellTemporatyChildren:
                case GroupLabel.CellVisibleDistandChildren:
                    return BitConverter.ToString(rawLabel.Reverse().ToArray()).Replace("-", "");
                case GroupLabel.InteriorCellBlock:
                case GroupLabel.InteriorCellSubBlock:
                    return BitConverter.ToInt32(rawLabel, 0);
                case GroupLabel.ExteriorCellBlock:
                case GroupLabel.ExteriorCellSubBlock:
                    var x = BitConverter.ToInt16(rawLabel, 0);
                    var y = BitConverter.ToInt16(rawLabel, 2);
                    return new short[,] { { x, y } };
                default:
                    return null;
            }
        }

        /// <summary>
        /// Builds Records or Groups
        /// </summary>
        public virtual void BuildRecords()
        {

            if (Data.Length == 0) return; //group has no records or subgroups

            var reader = new ByteReader();

            while (Data.Length != reader.offset)
            {
                var name = reader.ReadBytes<string>(Data, 4);
                var size = reader.ReadBytes<int>(Data);


                reader.offset -= 8;

                if (!name.Equals("GRUP"))
                {

                    Assembly assembly = Assembly.GetExecutingAssembly();
                    var rawRecord = reader.ReadBytes<byte[]>(Data, size + 20);
                    Record record = assembly
                        .CreateInstance($"TES4Lib.Records.{name}", false, BindingFlags.Default, null, new object[] { rawRecord }, null, null) as Record;

                    if (record != null && !String.IsNullOrEmpty(record.FormId))
                        TES4Lib.TES4.TES4RecordIndex.Add(record.FormId, record);
                    Records.Add(record);
                }

                else
                {
                    Groups.Add(new Group(reader.ReadBytes<byte[]>(Data, size)));
                }
            }
        }

        /// <summary>
        /// Special builder for WRLD group, for now targets only 1 world space + its children, doubt it works for plugins
        /// </summary>
        private void BuildWRLDGroup()
        {
            //find the WRLD we are looking for
            var reader = new ByteReader();
            while (Data.Length != reader.offset)
            {
                var name = reader.ReadBytes<string>(Data, 4);
                var size = reader.ReadBytes<int>(Data);
                reader.offset += 4;
                string FormId = reader.ReadFormId(Data);
                reader.offset -= 16;

                if (name.Equals("WRLD") && FormId.Equals("00009F18"))//hard coded SEWorld
                {
                    var WRLD = new Records.WRLD(reader.ReadBytes<byte[]>(Data, size + 20));
                    TES4Lib.TES4.TES4RecordIndex.Add(WRLD.FormId, WRLD);
                    Records.Add(WRLD);
                    continue;
                }
                if (name.Equals("GRUP") && PeekWorldChildren(reader.offset).Equals("00009F18")) //world children
                {
                    var WorldChildren = new Group(reader.ReadBytes<byte[]>(Data, size));
                    Groups.Add(WorldChildren);
                    break;
                }

                //move by offset
                if (!name.Equals("GRUP"))
                {
                    reader.offset += size + 20;
                    continue;
                }
                reader.offset += size;
            }
        }

        private string PeekWorldChildren(int offset)
        {
            var reader = new ByteReader();
            reader.offset = offset+8;
            string parent = reader.ReadFormId(Data);
            return parent;   
        }
    }
}
