using System;
using System.Linq;
using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XOWN : Subrecord
    {
        /// <summary>
        /// FormId
        /// </summary>
        public string Owner { get; set; }

        public XOWN(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Owner = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, base.Size).Reverse().ToArray()).Replace("-", "");

        }
    }
}
