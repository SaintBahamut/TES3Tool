using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Utility
{
    public class ByteReader
    {
        public int offset = 0;

        /// <summary>
        /// Its shit but works (boxing hell), will refactor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="bytesToRead"></param>
        /// <returns></returns>
        public T ReadBytes<T>(byte[] data, int? bytesToRead = null)
        {
            if (typeof(T) == typeof(ulong))
            {
                var converted = BitConverter.ToUInt64(data, offset);
                offset += sizeof(ulong);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (typeof(T) == typeof(float))
            {
                var converted = BitConverter.ToSingle(data, offset);
                offset += sizeof(float);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (typeof(T) == typeof(long))
            {
                var converted = BitConverter.ToInt64(data, offset);
                offset += sizeof(long);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (typeof(T) == typeof(int))
            {
                var converted = BitConverter.ToInt32(data, offset);
                offset += sizeof(int);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (typeof(T) == typeof(short))
            {
                var converted = BitConverter.ToInt16(data, offset);
                offset += sizeof(short);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (typeof(T) == typeof(ushort))
            {
                var converted = BitConverter.ToUInt16(data, offset);
                offset += sizeof(ushort);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (typeof(T) == typeof(uint))
            {
                var converted = BitConverter.ToUInt32(data, offset);
                offset += sizeof(uint);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (typeof(T) == typeof(byte[]) && bytesToRead != null)
            {
                var converted = data.Skip(offset).Take(bytesToRead.Value).ToArray();
                offset += bytesToRead.Value;
                return (T)(object)converted;
            }
            if (typeof(T) == typeof(string) && bytesToRead != null)
            {
                var converted = Encoding.ASCII.GetString(data.Skip(offset).Take((bytesToRead.Value)).ToArray());
                offset += bytesToRead.Value;
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (typeof(T) == typeof(byte))
            {
                var converted = data.Skip(offset).Take(1).ToArray()[0];
                offset += sizeof(byte);
                return (T)(object)converted;
            }
            if (typeof(T).IsEnum)
            {
                Type enumType = typeof(T);
                Type enumValueType = Enum.GetUnderlyingType(enumType);
                int enumValueSize = Marshal.SizeOf(enumValueType);

                switch (enumValueSize)
                {
                    case sizeof(uint):
                        var int32 = BitConverter.ToUInt32(data, offset);
                        offset += enumValueSize;
                        return (T)Enum.ToObject(enumType, int32);
                    case sizeof(ushort):
                        var int16 = BitConverter.ToUInt16(data, offset);
                        offset += enumValueSize;
                        return (T)Enum.ToObject(enumType, int16);
                    case sizeof(byte):
                        var byt = data.Skip(offset).Take(1).ToArray()[0];
                        offset += enumValueSize;
                        return (T)Enum.ToObject(enumType, byt);
                    default:
                        throw new Exception("From params to read enumerable");
                }
               
            }

            throw new Exception("Wrong input params");
        }

        /// <summary>
        /// Outputs set of flags from byte array
        /// </summary>
        /// <typeparam name="T">Flags Definition as Enum</typeparam>
        /// <param name="data">byte array</param>
        /// <returns></returns>
        public HashSet<T> ReadFlagBytes<T>(byte[] data) where T : Enum
        {
            Type enumType = typeof(T);
            Type enumValueType = Enum.GetUnderlyingType(enumType);
            int enumValueSize = Marshal.SizeOf(enumValueType);  

            uint converted = 0;
            switch (enumValueSize)
            {
                case sizeof(uint):
                    converted = BitConverter.ToUInt32(data, offset);
                    break;
                case sizeof(ushort):
                    converted = BitConverter.ToUInt16(data, offset);
                    break;
                case sizeof(byte):
                    converted = data.Skip(offset).Take(1).ToArray()[0];
                    break;
            }
            offset += enumValueSize;

            if (converted.Equals(0)) return new HashSet<T>();

            var setOfEnum = new HashSet<T>();

            var enumValues = Enum.GetValues(typeof(T));
            foreach (var enumVal in enumValues)
            {
                if ((converted & Convert.ToUInt32(enumVal)) != 0)
                {
                    setOfEnum.Add((T)Enum.ToObject(enumType, enumVal));
                }
            }
            return setOfEnum;

        }

        public void SetOffset(int newOffset)
        {
            offset = newOffset;
        }

        public void ShiftBackBy(int offsetShift)
        {
            offset -= offsetShift;
        }

        public void ShiftForwardBy(int offsetShift)
        {
            offset += offsetShift;
        }
    }
}
