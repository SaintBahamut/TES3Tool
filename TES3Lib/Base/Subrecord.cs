using System;
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
                .GetProperties(System.Reflection.BindingFlags.Public |
                               System.Reflection.BindingFlags.Instance |
                               System.Reflection.BindingFlags.DeclaredOnly)
                               .OrderBy(x => x.MetadataToken)
                               .ToList();

            List<byte> data = new List<byte>();
            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(this);
                try
                {
                    data.AddRange(ByteWriter.ToBytes(value, property.PropertyType));
                }
                catch (Exception)
                {

                    throw;
                }
              
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}
