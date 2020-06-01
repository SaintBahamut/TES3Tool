using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TES4Lib.Enums.Flags;
using Utility;
using static Utility.Common;
using TES4Lib.Subrecords.Shared;
using ComponentAce.Compression.Libs.zlib;

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

            if (Flag.Contains(RecordFlag.Compressed))
            {
                DecompressData(Data);
            }          
        }

        /// <summary>
        /// Builds SubRecords
        /// </summary>
        protected virtual void BuildSubrecords()
        {
            if (!IsImplemented) return;

            var reader = new ByteReader();
            while (Data.Length != reader.offset)
            {
                string subrecordName = GetSubrecordName(reader);
                int subrecordSize = GetSubrecordSize(reader);            

                try
                {
                    if (subrecordName.Equals("OFST") && !Name.Equals("HEDR"))
                    {
                        //better not go there
                        break;
                    }

                    ReadSubrecords(reader, subrecordName, subrecordSize);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building {this.GetType().ToString()} on {subrecordName} eighter not implemented or borked {e}");
                    break;
                }
            }
        }

        protected void ReadSubrecords(ByteReader readerData, string subrecordName, int subrecordSize)
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
                return;
            }
            object subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { readerData.ReadBytes<byte[]>(Data, subrecordSize) });
            subrecordProp.SetValue(this, subrecord);
        }

        protected string GetSubrecordName(ByteReader reader)
        {
            var name = reader.ReadBytes<string>(Data, TES4_RECORD_NAME_SIZE);
            reader.ShiftBackBy(TES4_RECORD_NAME_SIZE);
            return name;
        }

        /// <summary>
        /// Get EditorId of record if exists
        /// </summary>
        public virtual string GetEditorId()
        {
            PropertyInfo name = this.GetType().GetProperty("EDID");
            if (!IsNull(name))
            {
                var EDID = (EDID)name.GetValue(this);
                if (!IsNull(EDID))
                {
                    return EDID.EditorId;
                }             
            }

            return null;
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

        private void DecompressData(byte[] inData)
        {
            var reader = new ByteReader();
            Size = reader.ReadBytes<int>(inData);

            byte[] DataDecompressed;
            Decompress(reader.ReadBytes<byte[]>(inData, inData.Length-4), out DataDecompressed);

            if (!Size.Equals(DataDecompressed.Length))
            {
                throw new Exception("Failed to decompress record data");
            }
         
            Data = DataDecompressed;
        }

        public static void Decompress(byte[] inData, out byte[] outData)
        {
            using (MemoryStream outMemoryStream = new MemoryStream())
            using (ZOutputStream outZStream = new ZOutputStream(outMemoryStream))
            using (Stream inMemoryStream = new MemoryStream(inData))
            {
                CopyStream(inMemoryStream, outZStream);
                outZStream.finish();
                outData = outMemoryStream.ToArray();
            }
        }

        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            var bufferSize = 14000;
            byte[] buffer = new byte[bufferSize];
            int len;
            while ((len = input.Read(buffer, 0, bufferSize)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
    }
}
