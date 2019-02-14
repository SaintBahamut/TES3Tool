using System;
using System.Linq;
using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class XRTM : Subrecord
    {
        /// <summary>
        /// CELL reference ?
        /// </summary>
        public string CellFormId { get; set; }

        public XRTM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var cellFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            CellFormId = BitConverter.ToString(cellFormIdBytes.Reverse().ToArray()).Replace("-", "");
        }
    }
}
