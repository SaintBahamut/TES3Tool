using System;
using System.Text;
using Utility.Attributes;
using static Utility.Common;

namespace Utility
{
    public static class ByteWriter
    {
        static ByteWriter()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public static byte[] ToBytes(object data, Type type, SizeInBytesAttribute size = null)
        {
            byte[] bytes;
            if (type == typeof(byte[]))
                bytes = (byte[])data;
            else if (type == typeof(byte))
                bytes = new byte[] { Convert.ToByte(data) };
            else if (type == typeof(bool))
                bytes = new byte[] { Convert.ToByte(data) };
            else if (type == typeof(int))
                bytes = BitConverter.GetBytes(Convert.ToInt32(data));
            else if (type == typeof(uint))
                bytes = BitConverter.GetBytes((uint)data);
            else if (type == typeof(float))
                bytes = BitConverter.GetBytes((float)data);
            else if (type == typeof(short))
                bytes = BitConverter.GetBytes((short)data);
            else if (type == typeof(ushort))
                bytes = BitConverter.GetBytes((ushort)data);
            else if (type == typeof(string))
                bytes = WriteStringBytes((string)data);
            else if (type == typeof(long))
                bytes = BitConverter.GetBytes((long)data);
            else if (type == typeof(ulong))
                bytes = BitConverter.GetBytes((ulong)data);   
            else if (type.IsEnum)
                bytes = ToBytes(data, type.GetEnumUnderlyingType());
            else
                throw new Exception($"Unsupported conversion type of type {type}");

            if (IsNull(size))
                return bytes;

            Array.Resize(ref bytes, size.TypeSize);
            return bytes;
        }

        private static byte[] WriteStringBytes(string encodedString)
        {
            var fromEncoding = Encoding.Unicode;
            var toEncoding = Encoding.GetEncoding(TextEncodingCode);
            return Encoding.Convert(fromEncoding, toEncoding, fromEncoding.GetBytes(encodedString));
        }
    }
}
