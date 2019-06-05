using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using Utility;
using Utility.Attributes;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Inventory item
    /// </summary>
    [DebuggerDisplay("{ItemId} x {Count}")]
    public class NPCO : Subrecord
    {
        public int Count { get; set; }

        [SizeInBytes(32)]
        public string ItemId { get; set; }

        public NPCO()
        {
        }

        public NPCO(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Count = reader.ReadBytes<int>(base.Data);
            ItemId = reader.ReadBytes<string>(base.Data, 32);
        }

        //public override byte[] SerializeSubrecord()
        //{
        //    List<byte> data = new List<byte>();
        //    data.AddRange(ByteWriter.ToBytes(Count, Count.GetType()));

        //    byte[] itemIdBytes = ASCIIEncoding.ASCII.GetBytes(ItemId);
        //    Array.Resize(ref itemIdBytes, 32);
        //    data.AddRange(itemIdBytes);

        //    var serialized = Encoding.ASCII.GetBytes("NPCO")
        //       .Concat(BitConverter.GetBytes(data.Count))
        //       .Concat(data).ToArray();
        //    return serialized;
        //}
    }
}
