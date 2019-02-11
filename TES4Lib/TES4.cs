using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TES4Lib.Structures.Base;
using Utility;


namespace TES4Lib
{
    public class TES4
    {
        const int HeaderSize = 20;
        public Records.TES4 Tes4 { get; set; }
        public List<Group> Groups { get; set; }

        public TES4()
        {
            Groups = new List<Group>();
        }

        /// <summary>
        /// Reads ESM/ESP from given path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static TES4 TES4Load(string filePath)
        {
            var TES4 = new TES4();
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            TES4.Tes4 = ReadTES4Record(fileStream);

            
            var groupHeader = new byte[8];
            while (fileStream.Position != fileStream.Length)
            {
                var reader = new ByteReader();
                int size = ReadGroupSize(fileStream, reader, groupHeader);

                ReadGroup(TES4, fileStream, size);
                Console.WriteLine($"{fileStream.Position} of {fileStream.Length} ");
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
            TES4.Groups.Add(new Group(data));
        }

       
    }
}
