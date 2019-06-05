using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Utility;
using Utility.Attributes;

namespace TES3Lib.Base
{
    /// <summary>
    /// Base class for TES3 Subrecord
    /// </summary>
    abstract public class Subrecord
    {
        /// <summary>
        /// 4 letter subrecord name
        /// </summary>
        readonly public string Name;

        /// <summary>
        /// Subrecord size minus header
        /// </summary>
        protected int Size { get; set; }

        /// <summary>
        /// Subrecord data without header with length defnined in Size property
        /// </summary>
        protected byte[] Data { get; set; }

        /// <summary>
        /// Raw bytes of record (header+data)
        /// </summary>
        private byte[] RawData { get; set; }

        /// <summary>
        /// Will be removed, simple check if subrecord is implemented, if not it will use RawData
        /// when serialized back to bytes
        /// </summary>
        protected bool IsImplemented = true;

        /// <summary>
        /// Used for loading subrecord data from ESM/ESP
        /// </summary>
        /// <param name="rawData">raw byte array from ESP/ESM</param>
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
            Name = this.GetType().Name;

        }

        /// <summary>
        /// Serializes Subrecord into byte array
        /// Overwrite when subrecords needs specific serialization functions
        /// </summary>
        /// <returns>Byte array with serialized subrecord</returns>
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
                var sizeAttribute = property.GetCustomAttributes<SizeInBytesAttribute>().FirstOrDefault();

                //used for flags in subrecords
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(HashSet<>))
                {
                    Type enumType = property.PropertyType.GetGenericArguments()[0];
                    Type enumValueType = Enum.GetUnderlyingType(enumType);

                    data.AddRange(ByteWriter.ToBytes(SerializeFlag(value), enumValueType));
                    continue;
                }

                data.AddRange(ByteWriter.ToBytes(value, property.PropertyType, sizeAttribute));          
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }

        protected uint SerializeFlag(object value)
        {
            uint flag = 0;
            foreach (Enum flagElement in value as IEnumerable)
            {
                flag = flag | Convert.ToUInt32(flagElement);
            }

            return flag;
        }

        public override bool Equals(object obj)
        {
            var properties = GetType()
                .GetProperties(BindingFlags.Public |
                               BindingFlags.Instance |
                               BindingFlags.DeclaredOnly)
                               .OrderBy(x => x.MetadataToken)
                               .ToList();

            foreach (PropertyInfo property in properties)
            {
                var thisValue = property.GetValue(this);
                var objValue = obj != null ? property.GetValue(obj) : null;
                if (!thisValue.Equals(objValue))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
