using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CONT
{
    public class CNTO : Subrecord
    {
        /// <summary>
        /// FormId of item
        /// </summary>
        public string ItemId { get; set; }

        public int ItemCount { get; set; }

        public CNTO(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var baseFormIdBytes = reader.ReadBytes<byte[]>(base.Data, 4);
            ItemId = BitConverter.ToString(baseFormIdBytes.Reverse().ToArray()).Replace("-", "");
            ItemCount = reader.ReadBytes<int>(base.Data);
        }
    }
}
