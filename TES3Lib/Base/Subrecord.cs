using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Utility;

namespace TES3Lib.Base
{
    abstract public class Subrecord
    {
        readonly public string Name;
        public int Size { get; set; }
        protected byte[] Data { get; set; }
        private byte[] RawData { get; set; }

        protected bool IsImplemented = true;

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
            if (!IsImplemented) return RawData;

            var properties = this.GetType()
                .GetProperties(BindingFlags.Public |
                               BindingFlags.Instance |
                               BindingFlags.DeclaredOnly)
                               .OrderBy(x => x.MetadataToken)
                               .ToList();

            List<byte> data = new List<byte>();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(this);

                //used for flags in subrecords
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(HashSet<>))
                {
                    Type enumType = property.PropertyType.GetGenericArguments()[0];
                    Type enumValueType = Enum.GetUnderlyingType(enumType);

                    uint flag = 0;
                    foreach (Enum flagElement in value as IEnumerable)
                    {
                        flag = flag | Convert.ToUInt32(flagElement);
                    }
                    data.AddRange(ByteWriter.ToBytes(flag, enumValueType));
                    continue;
                }
           
                data.AddRange(ByteWriter.ToBytes(value, property.PropertyType));          
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}
