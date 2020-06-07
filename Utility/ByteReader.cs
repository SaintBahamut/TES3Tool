﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Utility.Common;

namespace Utility
{
    public class ByteReader
    {
        public int offset = 0;

        static ByteReader()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Its shit but works
        /// Read raw bytes from byte array, 
        /// object instance keeps info about offset and updates it after every read
        /// </summary>
        /// <typeparam name="T">type of data to read</typeparam>
        /// <param name="data">raw bytes</param>
        /// <param name="bytesToRead">Number of bytes to read</param>
        /// <returns></returns>
        public T ReadBytes<T>(byte[] data, int? bytesToRead = null)
        {
            Type t = typeof(T);
            if (t == typeof(ulong))
            {
                var converted = BitConverter.ToUInt64(data, offset);
                offset += sizeof(ulong);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (t == typeof(float))
            {
                var converted = BitConverter.ToSingle(data, offset);
                offset += sizeof(float);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (t == typeof(long))
            {
                var converted = BitConverter.ToInt64(data, offset);
                offset += sizeof(long);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (t == typeof(int))
            {
                var converted = BitConverter.ToInt32(data, offset);
                offset += sizeof(int);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (t == typeof(short))
            {
                var converted = BitConverter.ToInt16(data, offset);
                offset += sizeof(short);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (t == typeof(ushort))
            {
                var converted = BitConverter.ToUInt16(data, offset);
                offset += sizeof(ushort);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (t == typeof(uint))
            {
                var converted = BitConverter.ToUInt32(data, offset);
                offset += sizeof(uint);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (t == typeof(byte[]) && bytesToRead != null)
            {
                var converted = data.Skip(offset).Take(bytesToRead.Value).ToArray();
                offset += bytesToRead.Value;
                return (T)(object)converted;
            }
            if (t == typeof(string) && bytesToRead != null)
            {
                var converted = ReadStringBytes(data.Skip(offset).Take((bytesToRead.Value)).ToArray());
                offset += bytesToRead.Value;

                // Clean up garbage from the end of strings. We still need to preserve null characters so that they serialize correctly.
                string rawString = (Convert.ChangeType(converted, typeof(T)) as string);
                if (rawString.Contains('\0'))
                {
                    return (T)(object)rawString.Substring(0, rawString.IndexOf('\0')).PadRight(bytesToRead.Value, '\0');
                }
                else
                {
                    return (T)(object)rawString;
                }
            }
            if (t == typeof(byte))
            {
                var converted = data.Skip(offset).Take(1).ToArray()[0];
                offset += sizeof(byte);
                return (T)(object)converted;
            }
            if (t == typeof(sbyte))
            {
                var converted = data.Skip(offset).Take(1).ToArray()[0];
                offset += sizeof(sbyte);
                return (T)(object)unchecked((sbyte)converted);
            }
            if (t == typeof(bool))
            {
                var converted = data.Skip(offset).Take(1).ToArray()[0];
                offset += sizeof(bool);
                return (T)Convert.ChangeType(converted, typeof(T));
            }
            if (t.IsEnum)
            {
                Type enumValueType = Enum.GetUnderlyingType(t);
                int enumValueSize = Marshal.SizeOf(enumValueType);

                if (enumValueType == typeof(uint) && bytesToRead != null)
                {
                    var slicedData = data.Skip(offset).Take((int)bytesToRead).ToArray();
                    Array.Resize(ref slicedData, 4);

                    var converted = BitConverter.ToUInt32(slicedData, 0);
                    offset += (int)bytesToRead;
                    return (T)Enum.ToObject(t, converted);
                }
                if (enumValueType == typeof(uint))
                {
                    var converted = BitConverter.ToUInt32(data, offset);
                    offset += enumValueSize;
                    return (T)Enum.ToObject(t, converted);
                }
                if (enumValueType == typeof(int) && bytesToRead != null)
                {
                    var slicedData = data.Skip(offset).Take((int)bytesToRead).ToArray();
                    Array.Resize(ref slicedData, 4);

                    var converted = BitConverter.ToInt32(slicedData, 0);
                    offset += (int)bytesToRead;
                    return (T)Enum.ToObject(t, converted);
                }
                if (enumValueType == typeof(int))
                {
                    var converted = BitConverter.ToInt32(data, offset);
                    offset += enumValueSize;
                    return (T)Enum.ToObject(t, converted);
                }
                if (enumValueType == typeof(short))
                {
                    var converted = BitConverter.ToInt16(data, offset);
                    offset += enumValueSize;
                    return (T)Enum.ToObject(t, converted);
                }
                if (enumValueType == typeof(ushort))
                {
                    var converted = BitConverter.ToUInt16(data, offset);
                    offset += enumValueSize;
                    return (T)Enum.ToObject(t, converted);
                }
                if (enumValueType == typeof(byte))
                {
                    var converted = data.Skip(offset).Take(1).ToArray()[0];
                    offset += enumValueSize;
                    return (T)Enum.ToObject(t, converted);
                }
                throw new Exception("From params to read enumerable");
            }

            throw new Exception("Wrong input params");
        }

        public string ReadStringBytes(byte[] bytes)
        {
            var fromEncoding = Encoding.GetEncoding(TextEncodingCode);
            var toEncoding = Encoding.Unicode;
            return toEncoding.GetString(Encoding.Convert(fromEncoding, toEncoding, bytes));
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
                var flag = Convert.ToUInt32(enumVal);
                if ((converted & flag) == flag)
                {
                    setOfEnum.Add((T)Enum.ToObject(enumType, enumVal));
                }
            }
            return setOfEnum;

        }

        /// <summary>
        /// Reads oblivion formId
        /// </summary>
        /// <param name="data">raw bytes</param>
        /// <returns></returns>
        public string ReadFormId(byte[] data)
        {
            var formId = BitConverter.ToString(data.Skip(offset).Take(4).Reverse().ToArray()).Replace("-", "");
            offset += 4;
            return formId;
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
