using System.Collections.Generic;
using System.Reflection;
using Utility;

namespace TES3Lib.Structures.Base
{
    abstract public class Subrecord
    {
        private object serializedRecords;

        public string Name { get; set; }
        public int Size { get; set; }
        public byte[] Data { get; set; }
        private byte[] RawData { get; set; }

        public bool IsImplemented { get; set; }

        public Subrecord(byte[] rawData)
        {
            RawData = rawData;
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(RawData, 4);
            Size = reader.ReadBytes<int>(RawData);
            Data = reader.ReadBytes<byte[]>(RawData, (int)Size);
        }

        public Subrecord()
        {

        }

        public virtual byte[] SerializeSubrecord()
        {
            if (!IsImplemented)
                return RawData;

            var properties = this.GetType().GetProperties();
            var serializedSubrecords = new List<byte>();

            //name, size, arbitral shit

            foreach (PropertyInfo property in properties)
            {
                var subrecordData = (Subrecord)property.GetValue(this);



                serializedSubrecords.AddRange(subrecordData.SerializeSubrecord());
            }

            return serializedSubrecords.ToArray();
        }
    }
}
