using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.FURN
{
    public class MNAM : Subrecord
    {
        /// <summary>
        /// First letter marks
        /// 8 - bed
        /// 4 - chair
        /// </summary>
        public string ActiveMarkerFlags { get; set; }

        public MNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var baseFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            ActiveMarkerFlags = BitConverter.ToString(baseFormIdBytes.Reverse().ToArray()).Replace("-", "");
        }
    }
}