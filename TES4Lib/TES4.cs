using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TES4Lib.Base;
using Utility;


namespace TES4Lib
{

    public class TES4
    {
        const int HeaderSize = 20;
 
        public Records.TES4 Tes4 { get; set; }
        public ConcurrentBag<Group> Groups { get; set; }

        //Global table for quick access to objects
        public static Dictionary<string, Record> TES4RecordIndex = new Dictionary<string, Record>();

        public TES4()
        {
            Groups = new ConcurrentBag<Group>();
        }

        /// <summary>
        /// Reads ESM/ESP from given path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static TES4 TES4Load(string filePath, List<string> filteredGrops =null)
        {
            if (filteredGrops == null) filteredGrops = new List<string>(); 
          

            var TES4 = new TES4();
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            TES4.Tes4 = ReadTES4Record(fileStream);

            var groupHeader = new byte[12];
            List<Task> tasks = new List<Task>();
            while (fileStream.Position != fileStream.Length)
            {
                var reader = new ByteReader();
                int size = ReadGroupSize(fileStream, reader, groupHeader);
                string type = ReadGroupType(fileStream, reader, groupHeader);

                if (filteredGrops.Count > 0 && !filteredGrops.Contains(type))
                {
                    fileStream.Position += size;
                    continue;
                }
                

                var data = new byte[size];
                fileStream.Read(data, 0, data.Length);

                Task task = new Task(() =>
                {
                    var g = new Group(data);
                    TES4.Groups.Add(g);
                    Console.WriteLine($"group {g.Label} built with record count: {g.Records.Count}");
                });
                task.Start();
                tasks.Add(task);
            }

            foreach (Task task in tasks)
            {
                task.Wait();
            }

            return TES4;
        }

        /// <summary>
        /// Reads TES4 Record, contains header data for ESM/ESP
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        private static Records.TES4 ReadTES4Record(FileStream fileStream)
        {
            var headerDataSizeRaw = new byte[4];
            fileStream.Position = 4;
            fileStream.Read(headerDataSizeRaw, 0, headerDataSizeRaw.Length);
            fileStream.Position = 0;

            var reader = new ByteReader();
            var headerDataSize = reader.ReadBytes<int>(headerDataSizeRaw);
            var tes4 = new byte[HeaderSize + headerDataSize];
            fileStream.Read(tes4, 0, tes4.Length);
            return new Records.TES4(tes4);
        }

        /// <summary>
        /// Reads size of group, size=header+data
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="reader"></param>
        /// <param name="groupHeader"></param>
        /// <returns></returns>
        private static int ReadGroupSize(FileStream fileStream, ByteReader reader, byte[] groupHeader)
        {
            fileStream.Read(groupHeader, 0, groupHeader.Length);
            fileStream.Position -= groupHeader.Length;
            reader.ShiftForwardBy(4);
            var size = reader.ReadBytes<int>(groupHeader);
            return size;
        }

        /// <summary>
        /// Read group and add it to group list
        /// </summary>
        /// <param name="TES4"></param>
        /// <param name="fileStream"></param>
        /// <param name="size"></param>
        private static void ReadGroup(TES4 TES4, FileStream fileStream, int size)
        {
            var data = new byte[size];
            fileStream.Read(data, 0, data.Length);
     
            ThreadPool.QueueUserWorkItem(new WaitCallback((object a) =>
            {
                var g = new Group(data);
                TES4.Groups.Add(g);
                Console.WriteLine($"group {g.Label} built total: {TES4.Groups.Count}");
            

            }));
           
        }

        private static string ReadGroupType(FileStream fileStream, ByteReader reader, byte[] groupHeader)
        {
            fileStream.Read(groupHeader, 0, groupHeader.Length);
            fileStream.Position -= groupHeader.Length;
            var type = reader.ReadBytes<string>(groupHeader,4);
            return type;
        }

    }
}
