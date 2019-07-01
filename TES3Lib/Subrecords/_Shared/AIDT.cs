using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using Utility;
using Utility.Attributes;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// NPC AI data subrecord
    /// </summary>
    public class AIDT : Subrecord
    {
        public byte Hello { get; set; }

        public byte Unknown1 { get; set; }

        public byte Fight { get; set; }

        public byte Flee { get; set; }

        public byte Alarm { get; set; }

        public byte Unknown2 { get; set; }

        public byte Unknown3 { get; set; }

        public byte Unknown4 { get; set; }

        public HashSet<ServicesFlag> Flags { get; set; }

        public AIDT()
        {
        }

        public AIDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Hello = reader.ReadBytes<byte>(base.Data);
            Unknown1 = reader.ReadBytes<byte>(base.Data);
            Fight = reader.ReadBytes<byte>(base.Data);
            Flee = reader.ReadBytes<byte>(base.Data);
            Alarm = reader.ReadBytes<byte>(base.Data);
            Unknown2 = reader.ReadBytes<byte>(base.Data);
            Unknown3 = reader.ReadBytes<byte>(base.Data);
            Unknown4 = reader.ReadBytes<byte>(base.Data);
            Flags = reader.ReadFlagBytes<ServicesFlag>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {

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

                    var xserialized = ByteWriter.ToBytes(SerializeFlag(value), enumValueType);

                    data.AddRange(xserialized);
                    continue;
                }

                data.AddRange(ByteWriter.ToBytes(value, property.PropertyType, sizeAttribute));
            }

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}