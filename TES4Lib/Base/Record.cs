using System;
using Utility;

namespace TES4Lib.Structures.Base
{
    public class Record
    {
        #region Fields
        public string Name { get; set; }
        public int Size { get; set; }
        public int Flag { get; set; }
        public byte[] FormId { get; set; }
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
            Name = reader.ReadBytes<string>(RawData, 4);
            Size = reader.ReadBytes<int>(RawData);
            Flag = reader.ReadBytes<int>(RawData, 4);
            FormId = reader.ReadBytes<byte[]>(RawData, 8);
            Data = reader.ReadBytes<byte[]>(RawData, (int)Size);
        }

        /// <summary>
        /// Builds SubRecords
        /// </summary>
        protected virtual void BuildSubrecords()
        {
            var readerData = new ByteReader();
            while (Data.Length != readerData.offset)
            {
                try
                {
                    var subrecordName = GetRecordName(readerData);
                    var subrecordSize = GetRecordSize(readerData);
                    var subrecordProp = this.GetType().GetProperty(subrecordName);
                    var subrecordData = readerData.ReadBytes<byte[]>(Data, subrecordSize);
                    //figure out compression...

                    var subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { subrecordData });
                    subrecordProp.SetValue(this, subrecord);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building {this.GetType().ToString()} eighter not implemented or borked {e}");
                    break;
                }
            }
        }

        protected string GetRecordName(ByteReader reader)
        {
            var name = reader.ReadBytes<string>(Data, 4);
            reader.ShiftBackBy(4);
            return name;
        }

        protected int GetRecordSize(ByteReader reader)
        {
            reader.ShiftForwardBy(4);
            var size = reader.ReadBytes<int>(Data) + 8;
            reader.ShiftBackBy(8);
            return size;
        }

        protected bool IsEndOfData(ByteReader reader) => (reader.offset == Data.Length);
    }
}
