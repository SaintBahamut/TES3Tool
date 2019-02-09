using System;
using System.Linq;
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

            throw new Exception("Wrong input params");
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
