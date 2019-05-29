using System;
using System.Text;

namespace Utility
{
    public static class ByteWriter
    {
        public static byte[] ToBytes(object data, Type type)
        {
            if (type == typeof(byte[])) return (byte[])data;
            if (type == typeof(byte)) return new byte[] { Convert.ToByte(data) };
            if (type == typeof(bool)) return new byte[] { (byte)data };
            if (type == typeof(int)) return BitConverter.GetBytes(Convert.ToInt32(data));
            if (type == typeof(float)) return BitConverter.GetBytes((float)data);
            if (type == typeof(short)) return BitConverter.GetBytes((short)data);
            if (type == typeof(string)) return Encoding.ASCII.GetBytes((string)data);
            if (type == typeof(long)) return BitConverter.GetBytes((long)data);
            if (type == typeof(ulong)) return BitConverter.GetBytes((ulong)data);
            if (type == typeof(uint)) return BitConverter.GetBytes((uint)data);
            if (type.IsEnum) return ToBytes(data, type.GetEnumUnderlyingType());
   
            throw new Exception($"Unsupported conversion type of type {type}");
        }
    }
}
