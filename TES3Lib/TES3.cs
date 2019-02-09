using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib
{
    public class TES3
    {
        const int HeaderSize = 16;
        public List<Record> Records { get; set; }

        public TES3()
        {
            Records = new List<Record>();
        }

        public static TES3 TES3Load(string filePath)
        {    
            var TES3 = new TES3();
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        
            var header = new byte[HeaderSize];
            while (fileStream.Read(header, 0, HeaderSize) != 0)
            {
                fileStream.Position -= HeaderSize;

                var reader = new ByteReader();
                var name = reader.ReadBytes<string>(header, 4);
                var size = reader.ReadBytes<int>(header);

                var data = new byte[HeaderSize + size];
                fileStream.Read(data, 0, HeaderSize + size);

                TES3.Records.Add(BuildRecord(name, data));
                Console.WriteLine(name);
            }

            return TES3;
        }

        private static Record BuildRecord(string name, byte[] data)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Record record = assembly
                .CreateInstance($"TES3Lib.Records.{name}", false, BindingFlags.Default, null, new object[] { data }, null, null) as Record;
            return record;

        }

        public void TES3Save(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                foreach (var record in Records)
                {
                    var serializedRecord = record.SerializeRecord();

                    fs.Write(serializedRecord, 0, serializedRecord.Length);
                }
            }
        }
    }
}
