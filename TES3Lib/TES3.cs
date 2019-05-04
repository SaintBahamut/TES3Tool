using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TES3Lib.Base;
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

        public static TES3 TES3Load(string filePath, List<string> filteredGrops = null)
        {
            if (filteredGrops == null) filteredGrops = new List<string>(); 
       
            var TES3 = new TES3();
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        
            var header = new byte[HeaderSize];
            List<Task> tasks = new List<Task>();
            while (fileStream.Read(header, 0, HeaderSize) != 0)
            {
                fileStream.Position -= HeaderSize;

                var reader = new ByteReader();
                var name = reader.ReadBytes<string>(header, 4);
                var size = reader.ReadBytes<int>(header);

                if (!name.Equals("TES3") && filteredGrops.Count > 0 && !filteredGrops.Contains(name))
                {
                    fileStream.Position += +HeaderSize+size;
                    continue;
                }

                var data = new byte[HeaderSize + size];
                fileStream.Read(data, 0, HeaderSize + size);

                
                TES3.Records.Add(null);
                int index = TES3.Records.Count - 1;
                tasks.Add(new Task(() => RecordBuildTask(name, data, TES3.Records, index)));
                tasks[index].Start();
               

                Console.WriteLine(name);
            }

            Task.WaitAll(tasks.ToArray());
            return TES3;
        }

        public static void RecordBuildTask(string name, byte[] data, List<Record> records, int index)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Record record = assembly
                .CreateInstance($"TES3Lib.Records.{name}", false, BindingFlags.Default, null, new object[] { data }, null, null) as Record;
            records[index] = record;
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
