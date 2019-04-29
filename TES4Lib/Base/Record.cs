using System;
using System.Collections.Generic;
using System.Reflection;
using TES4Lib.Enums.Flags;
using Utility;
using static Utility.Common;

namespace TES4Lib.Base
{
    public class Record
    {
        private const ushort TES4_SUBRECORD_HEADER_SIZE = 6;
        private const int TES4_RECORD_NAME_SIZE = 4;

        #region Fields
        public string Name { get; set; }
        public int Size { get; set; }
        public HashSet<RecordFlag> Flag { get; set; }
        public string FormId { get; set; }
        public int VersionControlInfo { get; set; }
        public byte[] Data { get; set; }
        private byte[] RawData { get; set; }

        /// <summary>
        /// Just a switch to say from what source serialize
        /// </summary>
        protected bool IsImplemented = true;
        #endregion

        public Record(byte[] rawData)
        {
            RawData = rawData;
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(RawData, TES4_RECORD_NAME_SIZE);
            Size = reader.ReadBytes<int>(RawData);
            Flag = reader.ReadFlagBytes<RecordFlag>(RawData);
            FormId = reader.ReadFormId(RawData);
            VersionControlInfo = reader.ReadBytes<int>(RawData);
            Data = reader.ReadBytes<byte[]>(RawData, Size);
        }

        /// <summary>
        /// Builds SubRecords
        /// </summary>
        protected virtual void BuildSubrecords()
        {
            if (!IsImplemented) return;

            var readerData = new ByteReader();
            while (Data.Length != readerData.offset)
            {
                string subrecordName = GetSubrecordName(readerData);
                int subrecordSize = GetSubrecordSize(readerData);            

                try
                {
                    PropertyInfo subrecordProp = this.GetType().GetProperty(subrecordName);
                    if (subrecordProp.PropertyType.IsGenericType)
                    {
                        var listType = subrecordProp.PropertyType.GetGenericArguments()[0];
                        if (IsNull(subrecordProp.GetValue(this)))
                        {
                            var IListRef = typeof(List<>);
                            Type[] IListParam = { listType };
                            object subRecordList = Activator.CreateInstance(IListRef.MakeGenericType(IListParam));
                            subrecordProp.SetValue(this, subRecordList);
                        }
                        object sub = Activator.CreateInstance(listType, new object[] { readerData.ReadBytes<byte[]>(Data, subrecordSize) });

                        subrecordProp.GetValue(this).GetType().GetMethod("Add").Invoke(subrecordProp.GetValue(this), new[] { sub });
                        continue;
                    }
                    object subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { readerData.ReadBytes<byte[]>(Data, subrecordSize) });
                    subrecordProp.SetValue(this, subrecord);

                    if (subrecordName.Equals("OFST") && !Name.Equals("HEDR"))
                    {
                        //better not go there
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building {this.GetType().ToString()} on {subrecordName} eighter not implemented or borked {e}");
                    break;
                }
            }
        }

        protected string GetSubrecordName(ByteReader reader)
        {
            var name = reader.ReadBytes<string>(Data, TES4_RECORD_NAME_SIZE);
            reader.ShiftBackBy(TES4_RECORD_NAME_SIZE);
            return name;
        }

        protected ushort GetSubrecordSize(ByteReader reader)
        {
            reader.ShiftForwardBy(4);
            ushort size = reader.ReadBytes<ushort>(Data);
            size += TES4_SUBRECORD_HEADER_SIZE;
            reader.ShiftBackBy(TES4_SUBRECORD_HEADER_SIZE);
            return size;
        }

        protected bool IsEndOfData(ByteReader reader) => (reader.offset == Data.Length);
    }
}
