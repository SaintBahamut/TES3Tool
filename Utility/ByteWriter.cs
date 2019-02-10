using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ByteWriter
    {
        public static byte[] ToBytes(object data, Type type)
        {
            if (type == typeof(byte[])) return (byte[])data;
            if (type == typeof(int)) return BitConverter.GetBytes((int)data);
            if (type == typeof(float)) return BitConverter.GetBytes((float)data);
            if (type == typeof(string)) return Encoding.ASCII.GetBytes((string)data);
            if (type == typeof(long)) return BitConverter.GetBytes((long)data);
            if (type == typeof(ulong)) return BitConverter.GetBytes((ulong)data);
            if (type == typeof(uint)) return BitConverter.GetBytes((uint)data);

            throw new Exception("Wrong input params");
        }
    }
}
